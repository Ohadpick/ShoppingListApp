import { Item } from './item';

export interface Basket {
    id: number;
    title: string;
    description: string;
    basketItems: Item[];
}
