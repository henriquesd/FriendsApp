import { Photo } from './photo';

export interface User {
    id: number;
    username: string;
    // TODO: Change KnowAs to KnownAs;
    knowAs: string;
    age: number;
    gender: string;
    created: Date;
    lastActive: Date;
    photoUrl: string;
    city: string;
    country: string;
    interests?: string;
    introduction?: string;
    lookingFor?: string;
    photos?: Photo[];
}
