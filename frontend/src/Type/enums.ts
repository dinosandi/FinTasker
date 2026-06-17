
export const TaskStatus = {
    Todo: "Todo",
    InProgress: "InProgress",
    Review: "Review",
    Completed: "Completed",
    Cancelled: "Cancelled",
  } as const;
  export type TaskStatus = (typeof TaskStatus)[keyof typeof TaskStatus];
  export const TASK_STATUS_VALUES = Object.values(TaskStatus) as TaskStatus[];
  
  export const TaskPriority = {
    Low: "Low",
    Medium: "Medium",
    High: "High",
    Critical: "Critical",
  } as const;
  export type TaskPriority = (typeof TaskPriority)[keyof typeof TaskPriority];
  export const TASK_PRIORITY_VALUES = Object.values(
    TaskPriority,
  ) as TaskPriority[];
  
  export const MilestoneStatus = {
    Pending: "Pending",
    Completed: "Completed",
  } as const;
  export type MilestoneStatus =
    (typeof MilestoneStatus)[keyof typeof MilestoneStatus];
  
  export const ResourceStatus = {
    Available: "Available",
    InUse: "InUse",
    Maintenance: "Maintenance",
  } as const;
  export type ResourceStatus =
    (typeof ResourceStatus)[keyof typeof ResourceStatus];
  
  export const ActivityType = {
    Created: "Created",
    Updated: "Updated",
    StatusChanged: "StatusChanged",
    PriorityChanged: "PriorityChanged",
    Completed: "Completed",
    Deleted: "Deleted",
  } as const;
  export type ActivityType = (typeof ActivityType)[keyof typeof ActivityType];
  
  export const AuthProvider = {
    Local: "Local",
    Google: "Google",
  } as const;
  export type AuthProvider = (typeof AuthProvider)[keyof typeof AuthProvider];
  
  export const UserRole = {
    Admin: "Admin",
    Member: "Member",
  } as const;
  export type UserRole = (typeof UserRole)[keyof typeof UserRole];