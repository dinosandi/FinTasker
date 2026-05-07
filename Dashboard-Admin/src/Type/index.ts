// auth and users
export interface AuthLogin {
    email: string;
    passwordHash: string;
}

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