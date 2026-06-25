
export interface RegisterUser {
    email: string;
    passwordHash: string;
    name: string;
    phoneNumber: string;
    role: Role
}
// Type
export enum Role {
    admin = 1,
    user = 0,
}
// login with google
export interface GoogleLogin {
    idToken: string;
}



// Projects
export interface Project {
    Id : string;
    UsersId: string; // nanti ambil dari token/login user
    Name: string;
    Description: string;
    Status: ProjectStatus;
    Color: string;
    StartDate: Date;
    EndDate: Date;
}
export enum ProjectStatus {
    NotStarted,
    InProgress,
    Completed,
    Cancelled
}

export interface ProjectResponse {
    id: string;
    name: string;
    description: string;
    status: ProjectStatus;
    color: string;
    startDate: string;
    endDate: string;
    updatedAt: string;
    createdAt: string;
  }
export interface ProjectQueryParams{
    page: number;
    pageSize: number;
    search: string;
}

export interface CreateProject {
    name: string;
    description: string;
    status: ProjectStatus;
    color: string;
    startDate: Date;
    endDate: Date;
}


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

export enum TaskStatus {
    ToDo,
    InProgress,
    Review,
    Completed,
    Cancelled
}

export enum TaskPriority {
    Low,
    Medium,
    High,
    Critical
}
