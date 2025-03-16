open System

// Определение типа бинарного дерева
type Tree =
    | Node of string * Tree * Tree  // Узел: значение (строка), левое поддерево, правое поддерево
    | Empty                        // Пустой узел

// Функция fold для дерева
let rec treeFold f acc tree =
    match tree with
    | Node(data, left, right) ->
        // Применяем f к текущему значению и аккумулятору
        let newAcc = f data acc
        // Рекурсивно сворачиваем левое и правое поддеревья
        treeFold f (treeFold f newAcc left) right
    | Empty -> acc  // База рекурсии: возвращаем аккумулятор

// Функция для поиска элемента в дереве (без учёта регистра)
let containsElementIgnoreCase tree target =
    let targetLower = (target : string).ToLower()  // Приводим искомое значение к нижнему регистру
    treeFold (fun x acc -> acc || x.ToLower() = targetLower) false tree

// Создание бинарного дерева
let zodiacTree =
    Node("Овен",
        Node("Телец",
            Node("Близнецы", Empty, Empty),  
            Node("Рак", Empty, Empty)  
        ),
        Node("Лев",
            Node("Дева", Empty, Empty),
            Empty
        )
    )

// Ввод данных от пользователя
printfn "Введите знак зодиака, относящийся к северным:"
let userInput = Console.ReadLine()

// Поиск элемента в дереве (без учёта регистра)
let isFound = containsElementIgnoreCase zodiacTree userInput

// Вывод результата
if isFound then
    printfn "Знак зодиака '%s' найден в дереве." userInput
else
    printfn "Знак зодиака '%s' не найден в дереве." userInput