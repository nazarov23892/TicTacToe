
class GameStatuses {
    static get WaitPlayer1() { return 0; };
    static get WaitPlayer2() { return 1; }
    static get Draw() { return 2; };
    static get WinPlayer1() { return 3 };
    static get WinPlayer2() { return 4 };
}

const FIELD_DIMENSION = 3;
let playerId = null;
let updateButton = document.getElementById("updateButton");
let fieldPoints = getFieldPoints();
let createGameButton = document.getElementById("createGameButton");
let gameId_input = document.getElementById("gameId_input");
let statusInput = document.getElementById("statusInput");
let messageInput = document.getElementById("messageInput");
let existGameId_Input = document.getElementById("existGameId_Input");
let connectButton = document.getElementById("connectButton");

updateButton.onclick = updateButton_Click;
createGameButton.onclick = createButton_Click;
connectButton.onclick = connectButton_Click;

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

function updateButton_Click(e) {

}

function createButton_Click(e) {

}

function connectButton_Click(e) {

}