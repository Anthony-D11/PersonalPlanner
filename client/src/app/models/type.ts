export type TodoType = {
    id: number;
    userId: number;
    title: string;
    description: string;
    list: ListType | null;
    activeDate: string;
    dueDate: string;
    tag: Array<TagType>;
    subTodos: Array<TodoType>;
    completed: boolean;
}
export type NewTodoType = {
    userId: number;
    title: string;
    description: string;
    list: ListType | null;
    activeDate: string;
    dueDate: string;
    tag: Array<TagType>;
    subTodos: Array<TodoType>;
    completed: boolean;
}
export type ListType = {
    id: number;
    userId: number;
    name: string;
    color: string;
    todoList: Array<TodoType>;
}
export type TagType = {
    id: number;
    userId: number;
    name: string;
    color: string;
    todoList: Array<TodoType>;
}
export type UserType = {
    id: number;
    name: string;
    userName: string;
    todoList: Array<TodoType>;
}