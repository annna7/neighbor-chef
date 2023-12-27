export interface Meal {
  name: string;
  description: string;
  pictureUrl?: string;
  price: number;
  ingredients: string[];
  category: string;
  chefId: string;
}
