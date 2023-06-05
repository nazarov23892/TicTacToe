## Тестовое задание
Спроектируйте и реализуйте REST API для игры в крестики нолики.    
Для разработки сайта и мобильного приложения для игры в крестики нолики 3x3 для двух игроков требуется реализовать web api. Игра проходит по обычным правилам.

Формат решения    
Платформа dotNet
Любая БД, допустимо просто использование файлов
Обязательно: проект должен быть выложен на GitHub и открываться с помощьюм VS2022
в readme.md репозитория или на выделенной онлайн странице должно быть описание API

## Описание API

### Команды
<table>
<tr>
  <td>команда</td>
  <td>method</td>
  <td>url</td>
  <td>request body (json)</td>
  <td>response body (json)</td>		 	
</tr>
<tr>
  <td>создать новую игру</td>
  <td> 
  
  `POST` 
  
  </td>
  <td> 
  
  `/api/game/` 
  
  </td>
  <td> 
  
  `empty` 
  
  </td>
  <td>если успешно:
    
```json
{
  "done": true,
  "gameId": "value",
  "player1_Id": "value"
}
```
если были ошибки:
```json
{
  "done": false,
  "errorMessage": "value"
}
```
  </td>
</tr>
<tr>
  <td>подключиться к существующей игре</td>
  <td> 
  
  `PUT` 
  
  </td>
  <td>   
    
  `/api/game/connect/gameId` 
  где `gameId` - индентификатор 
    
  </td>
  <td> 
  
  `empty` 
  
  </td>
  <td>если успешно:
  
```json
{
  "done": true,
  "player2_Id": "value"
}
```
если были ошибки:
```json
{
  "done": false,
  "errorMessage": "value"
}
```
  </td>
</tr>
<tr>
  <td>получить состояние игры</td>
  <td> 
  
  `GET` 
  
  </td>
  <td>   
    
  `/api/game/gameId` 
  где `gameId` - индентификатор 
    
  </td>
  <td> 
  
  `empty` 
  
  </td>
  <td>если успешно:
  
```json
{
  "done": true
  "status": 0,
  "points": [
    {
      "x": 0,
      "y": 0,
      "value": 0
    },
    {
      "x": 1,
      "y": 0,
      "value": 0
    }
  ]
}
```
если были ошибки:
```json
{
  "done": false,
  "errorMessage": "value"
}
```
  </td>
</tr>
<tr>
  <td>сделать ход</td>
  <td> 
  
  `PUT` 
  
  </td>
  <td>   
    
  `/api/game/gameId` 
  где `gameId` - индентификатор 
    
  </td>
  <td> 
  
```json
{
  "playerId": "value",
  "x": "1",
  "y": "2",
}
```
  </td>
  <td>если успешно:
  
```json
{
  "done": true
}
```
если были ошибки:
```json
{
  "done": false,
  "errorMessage": "value"
}
```
  </td>
</tr>
<tr>
  <td>сбросить игру</td>
  <td> 
  
  `PUT` 
  
  </td>
  <td>   
    
  `/api/game/reset/gameId` 
  где `gameId` - индентификатор 
    
  </td>
  <td> 
  
```json
{
  "playerId": "value"
}
```
  </td>
  <td>если успешно:
  
```json
{
  "done": true
}
```
если были ошибки:
```json
{
  "done": false,
  "errorMessage": "value"
}
```
  </td>
</tr>
</table>

### Параметры команд и ответов

#### Point
Используется при получении состояния игры и при выполнении хода
```json
{
  "x": 0,
  "y": 2,
  "value": 0
}
```
Описывает состояние точки игрового поля 
* `X` - столбец (значение 0..2) 
* `Y` - строка (значение 0..2) 
* `value` - состояние 
  * `0` - свободна
  * `1` - ход игроком1
  * `2` - ход игроком2

#### Status
Используется при получении состояния игры
```json
{
  "done": true
  "status": 0,
  "points": [
    {
      "x": 0,
      "y": 0,
      "value": 0
    }
  ]
}
```
Описывает состояние игры 
* `0` - ожидание подключения игрока2 
* `1` - ожидание хода игрока1 
* `2` - ожидание хода игрока2 
* `3` - ничья 
* `4` - победил игрок1 
* `5` - победил игрок2

## Тестовый фронтенд
В проект добавлена HTML-страница с Javascript-кодом, которую можно использовать для демонстрации работы игры.
Для доступа к этой странице необходимо запросить url '/FrontendPage.html'

![image](https://github.com/nazarov23892/TicTacToe/assets/116874442/3abf257b-ff6c-4346-8d1f-fe7fdc280b10)



