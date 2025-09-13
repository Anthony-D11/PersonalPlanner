export interface Todo {
    id: number;
    userId: number;
    title: string;
    description: string;
    listId: number | null;
    activeDate: string;
    dueDate: string;
    tag: Array<TodoTag>;
    subTodos: Array<Todo>;
    completed: boolean;
}
export interface NewTodo {
    userId: number;
    title: string;
    description: string;
    listId: number | null;
    activeDate: string;
    dueDate: string;
    tag: Array<TodoTag>;
    subTodos: Array<Todo>;
    completed: boolean;
}
export interface TodoList{
    id: number;
    userId: number;
    name: string;
    color: string;
    numTodos: number;
}
export interface NewTodoList {
    userId: number;
    name: string;
    color: string;
    numTodos: number;
}
export interface TodoTag {
    id: number;
    userId: number;
    name: string;
    color: string;
    numTodos: number;
}
export interface NewTodoTag {
    userId: number;
    name: string;
    color: string;
    numTodos: number;
}
export interface User {
    id: number;
    name: string;
    userName: string;
    todos: Array<Todo>;
}
