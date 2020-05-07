import { Category } from './category';
import { UnitOfMeasure } from './unitOfMeasure';

export interface Item {
    id: number;
    description: string;
    photoUrl: string;
    price: number;
    category: Category;
    unitOfMeasure: UnitOfMeasure;
}
