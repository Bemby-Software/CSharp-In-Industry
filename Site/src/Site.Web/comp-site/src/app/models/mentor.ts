import { ISocialLink } from "./social";

export interface IMentor {
    name: string;
    snapline: string;
    imageUrl: string;
    socials: ISocialLink[];
}