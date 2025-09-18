export interface Todo {
    id: number;
    title: string;
    description: string;
    activeDate: string;
    dueDate: string;
    tagIds: Array<number>;
    categoryId: number | null;
    subTodos: Array<Todo>;
    completed: boolean;
}
export interface NewTodo {
    title: string;
    description: string;
    activeDate: string;
    dueDate: string;
    tagIds: Array<number>;
    categoryId: number | null;
    subTodos: Array<Todo>;
    completed: boolean;
}
export interface Category{
    id: number;
    name: string;
    color: string;
    todoIds: Array<number>;
}
export interface NewCategory {
    name: string;
    color: string;
}
export interface Tag {
    id: number;
    name: string;
    color: string;
    todoIds: Array<number>;
}
export interface NewTag {
    name: string;
    color: string;
}
export interface User {
    id: number;
    name: string;
    userName: string;
    todos: Array<Todo>;
}
