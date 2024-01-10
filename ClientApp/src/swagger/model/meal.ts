import {Category} from "./category";

export interface Meal {
  id: string;
  name: string;
  description: string;
  pictureUrl?: string;
  price: number;
  ingredients: string[];
  categoryId: string;
  categoryName: string;
  chefId: string;
}

