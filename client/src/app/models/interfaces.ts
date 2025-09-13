export interface Todo {
    id: number;
    userId: number;
    title: string;
    description: string;
    list: TodoList | null;
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
    list: TodoList | null;
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
    todos: Array<Todo>;
}
export interface NewTodoList {
    userId: number;
    name: string;
    color: string;
    todos: Array<Todo>;
}
export interface TodoTag {
    id: number;
    userId: number;
    name: string;
    color: string;
    todos: Array<Todo>;
}
export interface NewTodoTag {
    userId: number;
    name: string;
    color: string;
    todos: Array<Todo>;
}
export interface User {
    id: number;
    name: string;
    userName: string;
    todos: Array<Todo>;
}
