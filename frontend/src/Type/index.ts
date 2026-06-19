
export interface RegisterUser {
    email: string;
    passwordHash: string;
    name: string;
    phoneNumber: string;
    role: Role
}
// Type
export type Role = 'admin' | 'user';

// login with google
export interface GoogleLogin {
    idToken: string;
}



// Projects
export interface Project {
    UsersId: string; // nanti ambil dari token/login user
    Name: string;
    Description: string;
    Status: ProjectStatus;
    Color: string;
    StartDate: Date;
    EndDate: Date;
}

export type ProjectStatus = 'NotStarted' | 'InProgress' | 'Completed' | 'Cancelled';

// Tasks
export interface Tasks {
    ProjectId: string;
    Title: string;
    Description: string;
    Status: TaskStatus;
    Priority: TaskPriority;
    DueDate: string;
    CompletedAt : string;
    Estimed_Minutes : number;
    UpdatedAt : string;
    CreatedAt : string;
}

export type TaskStatus = 'ToDo' | 'InProgress' | 'Review' | 'Completed' | 'Cancelled';
export type TaskPriority = 'Low' | 'Medium' | 'High' | 'Critical';

