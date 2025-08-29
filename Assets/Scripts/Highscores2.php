<?php
const SERVERNAME = "127.0.0.1";
const USERNAME = "username";
const PASS = "password";
const DBNAME = "db_name";

class HighScores
{
    private $conn;

    function __construct()
    {
        $this->conn = new mysqli(SERVERNAME, USERNAME, PASS, DBNAME);
        if ($this->conn->connect_error) {
            die("Connection failed: " . $this->conn->connect_error);
        }
    }

    function __destruct()
    {
        $this->conn->close();
    }

    function queryHighScores()
    {
        if ($this->conn) {
            $reply = array("items" => array());
            if (($result = $this->conn->query("SELECT * FROM HIGHSCORES ORDER BY score DESC LIMIT 3"))) {
                while (($row = $result->fetch_assoc())) {
                    $reply["items"][] = $row;
                }
                $result->close();
            } else {
                // Handle errors if needed
            }
            return $reply;
        }
        return null;
    }

    function insertHighScore($playername, $score, $playtime)
    {
        if ($this->conn) {
            $stmt = $this->conn->prepare("INSERT INTO HIGHSCORES (playername, score, playtime) VALUES (?, ?, ?)");
            $stmt->bind_param("sds", $playername, $score, $playtime);
            $ret = $stmt->execute();
            $stmt->close();
            return $ret;
        }
        return false;
    }
}

$response = array("status" => "error", "dbg" => "Undefined error");

$hs = new HighScores();

// Handle GET request
if ($_SERVER['REQUEST_METHOD'] === 'GET') {
    $result = $hs->queryHighScores();
    if ($result) {
        $response["status"] = "success";
        $response["items"] = $result["items"];
        $response["dbg"] = "GET request received";
    } else {
        $response["status"] = "error";
        $response["dbg"] = "GET request failed";
    }
    echo json_encode($response);
}

// Handle POST request
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $data = file_get_contents('php://input');
    $data_decoded = urldecode($data);
    $hsItem = json_decode($data_decoded, true);
    $ret = $hs->insertHighScore($hsItem['playername'], $hsItem['score'], $hsItem['playtime']);
    $response["status"] = $ret ? "success" : "error";
    $response["dbg"] = "POST received:" . $hsItem['playername'] . ': ' . $hsItem['score'] . ' at ' . $hsItem['playtime'];
    echo json_encode($response);
}
?>