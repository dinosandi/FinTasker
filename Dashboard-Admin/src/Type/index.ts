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

export interface ProjectDetail {
    id: string
    name: string
    description: string
    status: ProjectStatus
    color: string
    startDate: string
    endDate: string
    createdAt: string
    updatedAt: string
  
    tasks: TaskResponse[]
  }
// Tasks
export interface TaskResponse {
    id: string
    projectId: string
    projectName: string
    title: string
    description: string
    status: TaskStatus
    priority: TaskPriority
    dueDate: string
    completedAt: string | null
    estimatedMinutes: number
  
    createdAt: string
    updatedAt: string
  
    checklists: []
    tags: []
    timeLogs: []
    activities: []
    pomodoroSessions: []
    resources: []
  
    totalChecklistItems: number
    completedChecklistItems: number
    totalLoggedMinutes: number
    totalPomodoroMinutes: number
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

export interface Task {
    projectId: string
    title: string
    description: string
    priority: number
    status: number
    dueDate: string
    completedAt?: string
    estimed_Minutes: number
  }
  