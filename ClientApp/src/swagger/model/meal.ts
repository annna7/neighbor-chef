export interface Meal {
  id: string;
  name: string;
  description: string;
  pictureUrl?: string;
  price: number;
  ingredients: string[];
  categoryName: string;
  categoryId: string;
  chefId: string;
}

