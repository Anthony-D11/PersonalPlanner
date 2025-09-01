export type TodoType = {
    id: number | null;
    userId: number | null;
    title: string | null;
    description: string | null;
    list: ListType | null;
    activeDate: string | null;
    dueDate: string | null;
    tag: TagType | null;
    subTodos: Array<TodoType>;
    completed: boolean | null;
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