import { User } from "../models/User";

export interface UserState {
    users: User[];
    filter:{
        role: string;
        search: string;
    }
    loading: boolean; 
    error: string | null; 
}

export const initialUserState: UserState = {
    users: [
        new User(1, 'Arjun Reddy', 'arjun.reddy@example.com', 'admin', 'password@123'),
        new User(2, 'Priya Iyer', 'priya.iyer@example.com', 'user', 'secure@pass'),
        new User(3, 'Karthik Nair', 'karthik.nair@example.com', 'moderator', 'karthik@123'),
        new User(4, 'Meenakshi Sundaram', 'meena.sundaram@example.com', 'user', 'meena@456'),
        new User(5, 'Suresh Gowda', 'suresh.gowda@example.com', 'user', 'gowda@789'),
        new User(6, 'Lakshmi Menon', 'lakshmi.menon@example.com', 'manager', 'lakshmi@2023'),
        new User(7, 'Vijayakrishna', 'vijay.krishna@example.com', 'user', 'vijay@krishna'),
        new User(8, 'Ananya Pillai', 'ananya.pillai@example.com', 'moderator', 'ananya!pillai'),
        new User(9, 'Rajendra Cholan', 'raj.cholan@example.com', 'admin', 'cholan@empire'),
        new User(10, 'Divya Prabhakar', 'divya.p@example.com', 'user', 'divya@1234')
    ],
    loading: false,
    error: null,
    filter: {
        role: 'all',
        search: ''
    }
};

