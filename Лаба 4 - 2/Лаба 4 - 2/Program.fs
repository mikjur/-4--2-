open System

// Определение типа дерева
type Tree =
    | Node of string * Tree * Tree 
    | Empty                        

// Функция fold для дерева
let rec treeFold f acc tree =
    match tree with
    | Node(data, left, right) ->
        let newAcc = f data acc
        treeFold f (treeFold f newAcc left) right
    | Empty -> acc  

// Поиск элемента в дереве (игнорируя регистр)
let containsElementIgnoreCase tree target =
    let targetLower = (target : string).ToLower()  
    treeFold (fun x acc -> acc || x.ToLower() = targetLower) false tree

// Функция для ввода дерева с клавиатуры
let rec inputTree () =
    printfn "Введите знак зодиака (или 'stop' для завершения ввода):"
    let input = Console.ReadLine()
    if input = "stop" then
        Empty
    else
        printfn "Введите левое поддерево для знака '%s':" input
        let left = inputTree ()
        printfn "Введите правое поддерево для знака '%s':" input
        let right = inputTree ()
        Node(input, left, right)

// Основная программа
[<EntryPoint>]
let main argv =
    printfn "Введите дерево знаков зодиака:"
    let tree = inputTree ()  

    printfn "\nВведите знак зодиака, который хотите найти:"
    let userInput = Console.ReadLine()

    
    let isFound = containsElementIgnoreCase tree userInput
    if isFound then
        printfn "Знак зодиака '%s' найден в дереве." userInput
    else
        printfn "Знак зодиака '%s' не найден в дереве." userInput

    0 // Возвращаем код завершения программы