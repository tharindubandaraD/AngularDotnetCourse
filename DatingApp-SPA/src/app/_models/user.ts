import { Photo } from './Photo';

export interface User {
    id: number;
    username: string;
    KnownAs: string;
    age: number;
    gender: string;
    created: Date;
    lastActive: Date;
    photoUrl: string;
    city: string;
    country: string;
    interset?: string;
    introduction?: string;
    lookingFor?: string;
    photos?: Photo[];
}
