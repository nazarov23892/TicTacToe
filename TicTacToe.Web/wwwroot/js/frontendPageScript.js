
class GameStatuses {
    static get WaitConnectPlayer2() { return 0; };
    static get WaitPlayer1() { return 1; };
    static get WaitPlayer2() { return 2; }
    static get Draw() { return 3; };
    static get WinPlayer1() { return 4; };
    static get WinPlayer2() { return 5; };

    static getStatusText(statusCode) {
        switch (statusCode) {
            case GameStatuses.WaitConnectPlayer2: return "wait player #2 connect";
            case GameStatuses.WaitPlayer1: return "wait player #1 turn";
            case GameStatuses.WaitPlayer2: return "wait player #2 turn";
            case GameStatuses.WinPlayer1: return "player #1 WIN";
            case GameStatuses.WinPlayer2: return "player #2 WIN";
            case GameStatuses.Draw: return "draw";
            default: return "invalid state";
        }
    }
}

class PointValues {
    static get None() { return 0; };
    static get Player1() { return 1; };
    static get Player2() { return 2; };
}

const FIELD_DIMENSION = 3;
let playerId = null;
let updateButton = document.getElementById("updateButton");
let fieldPoints = getFieldPoints();
let createGameButton = document.getElementById("createGameButton");
let gameId_input = document.getElementById("gameId_input");
let playerId_input = document.getElementById("palayerId_input");
let statusInput = document.getElementById("statusInput");
let messageInput = document.getElementById("messageInput");
let existGameId_Input = document.getElementById("existGameId_Input");
let connectButton = document.getElementById("connectButton");

updateButton.onclick = updateButton_Click;
createGameButton.onclick = createButton_Click;
connectButton.onclick = connectButton_Click;
setFieldPoints_onClick();

// returns point elements as 2-dimensional array[x][y]
function getFieldPoints() {
    let points1 = [];
    const dimension = 3;
    for (var i = 0; i < dimension; i++) {
        let arr2 = [];
        points1.push(arr2);
    }
    for (let point1 of document.getElementsByClassName("fieldpoint")) {
        let posX = Number.parseInt(point1.getAttribute("data-posX"));
        let posY = Number.parseInt(point1.getAttribute("data-posY"));
        if (isNaN(posX) || isNaN(posY)) {
            continue;
        }
        points1[posX][posY] = point1;
    }
    return points1;
}

function setFieldPoints_onClick(fieldPointsArray) {
    for (let y = 0; y < FIELD_DIMENSION; y++) {
        for (let x = 0; x < FIELD_DIMENSION; x++) {
            fieldPoints[x][y].onclick = fieldPoint_Click;
        }
    }
}

async function fieldPoint_Click(e) {
    let x = Number.parseInt(e.target.getAttribute("data-posX"));
    let y = Number.parseInt(e.target.getAttribute("data-posY"));
    await doTurn(x, y);
}

async function updateButton_Click(e) {
    await updateState();
}

async function createButton_Click(e) {
    await createNewGame();
}

async function connectButton_Click(e) {
    await connectToGame();
}

async function createNewGame() {

    clearElements();
    let url = `/api/game/`;
    let request = {
        method: "POST",
        headers: {
            "Accept": "application/json",
        }
    };
    let response = await fetch(url, request);
    if (!response.ok) {
        return;
    }
    let result = await response.json();
    if (!result.done) {
        messageInput.value = result.error;
        return;
    }
    gameId_input.value = result.gameId;
    playerId_input.value = result.player1_Id;
}

async function updateState() {
    clearElements();

    let gameId = gameId_input.value;
    if (gameId == null || gameId === "") {
        statusInput.value = "unconnected";
        return;
    }
    let url = `/api/game/${gameId}`;
    let response = await fetch(url);
    if (!response.ok) {
        statusInput.value = "unconnected";
        return;
    }
    let gameState = await response.json();
    if (!gameState.done) {
        statusInput.value = "has errors";
        messageInput.value = gameState.error;
        return;
    }
    let statusCode = gameState.status;
    let statusText = GameStatuses.getStatusText(statusCode);
    for (let point of gameState.points) {
        let x = point.x;
        let y = point.y;
        let pointElement = fieldPoints[x][y];

        let sign = "";
        switch (point.value) {
            case PointValues.None:
                sign = ".";
                break;
            case PointValues.Player1:
                sign = "X";
                break;
            case PointValues.Player2:
                sign = "O";
                break;
            default:
                sign = "";
        }
        pointElement.innerText = sign;
    }
    statusInput.value = statusText;
}

async function connectToGame() {

    let gameId = existGameId_Input.value;
    clearElements();
    if (gameId == null && gameId == "") {
        return;
    }
    let url = `/api/game/connect/${gameId}`;
    let request = {
        method: "PUT",
        headers: {
            "Accept": "application/json",
        }
    };
    let response = await fetch(url, request);
    if (!response.ok) {
        return;
    }
    let result = await response.json();
    if (!result.done) {
        messageInput.value = result.error;
        return;
    }
    gameId_input.value = gameId;
    playerId_input.value = result.player2_Id;
}

async function doTurn(xPos, yPos) {
    let gameId = gameId_input.value;
    let playerId = palayerId_input.value;
    if (gameId == null || gameId == "") {
        messageInput.value = "unconnected";
        return;
    }
    let url = `/api/game/${gameId}`;
    let turnDto = {
        playerId: playerId,
        x: xPos,
        y: yPos
    };
    let request = {
        method: "PUT",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify(turnDto)
    };
    let response = await fetch(url, request);
    if (!response.ok) {
        messageInput.value = "connection lost";
        return;
    }
    let result = await response.json();
    if (!result.done) {
        messageInput.value = result.error;
        return;
    }
}

function clearElements() {

    statusInput.value = null;
    messageInput.value = null;
    existGameId_Input.value = null;

    for (var y = 0; y < FIELD_DIMENSION; y++) {
        for (var x = 0; x < FIELD_DIMENSION; x++) {
            let fieldPoint = fieldPoints[x, y];
            fieldPoint.innerText = null;
        }
    }
}