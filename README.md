# AbysLand

Это документация посвящается всем участникам проекта.
В ней вы можете увидеть мотивацию архитектуры, чтобы лучше ее понять!
И конечно же документация будет обновляться...

## Модули в проекте

- Player - все, что связано с игроком, то есть это его character, Inventory, ToolSystem(игрок контролирует тул).
- World - все, что связано с миром, то есть генерация, а в последствие хранение и доступ к миру.
- Resource - посути это реестр ресурсов, нужный чтобы получить к ним доступ из нескольких модулей.

Каждый модуль будет расмотрен далее...

## Player
Character - это оболочка игрока, от RigidBody2D до GameObjectContext 
(нужен, чтобы удобно получить [сабконтейнеру](https://github.com/Baccanno/Zenject/blob/master/Documentation/SubContainers.md) GameObject)

Inventory - это обычное MV* обьект, который содержит данные, логику и отображение

ToolSystem - GameObjects, привязанные к руке игрока, ими управляет компонент Hand, 
в которую пропихивается Resource(будет расмотрен далее)

PlayerFacade - это класс, который содержит только высокоуровневую логику, также является точкой доступа для других модулей

Перечисление, что вам возможно понадобится в будущем:
1) HealSystem - удобный инструмент для хила игрока, сложность хила заключается в том, 
что ты хилишь определенное количество жизней за определенное время, при этом надо все это как-то надо пропихивать в модель,
это конечно можно было бы сделать, но сильное дублирование кода и костыльность.
api:
HealByTime(string name, int health, float deltaTime = 1) - этот метод хилит игрока каждое deltaTime на health все время,
то есть если вы перехотите хилить игрока, например если у него максимальное хп, то вызывайте StopHealByName(string name)
если что name нужно чтобы идентифицировать запрос, также есть методы:
bool IsHealedByName(string name) - проверяет активен ли запрос(хилл)
UnLockByName \ LockByName(string name) - иногда нужно запретить хапрос, но при этом не останавливая, 
например: у игрока еда упала меньше 70 => прекратить хилл, но при этом запрос останется живым, просто не будет обрабатываться.

## Resource
Здесь будет достаточно интересно

сам по себе Resource - это ScriptableObject, который содержит поля данных и поле поведения(Tool)
В отдельном модуле есть реестр, где каждый Ресурс привязан к типу определенного тула

каждый Tool - это монобех сейчас рассмотрим цикл тула на одном примере:
Игрок сломал камень - выпал GameObject(обертка ресура) - при касании, прибавляется в инвентарь ресурс - 
игрок берет его в тулбар - тулбар говорит Hand, что equip и передает ресурс - TooSystem вытаскивает префаб Toolа у Resource -
создается Tool, прокидывается все зависимости и начинается его внутренний цикл(удар, поставить блок) -
дальше идет уничтожение тула при определенных условиях(закончились блоки, переключились на другой тул)

## World
UpperWorldGen - генератор верхнего мира - монобех,
TilemapPlayerInteraction - класс, отвечающий за взаимодействие игрока с 2д интовым массивом, в котором хранятся прочности всех тайлов.
IWorld - интерфейс для генерации всех миров (левелов?).
