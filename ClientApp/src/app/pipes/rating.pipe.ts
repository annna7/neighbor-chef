import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'rating',
})
export class RatingPipe implements PipeTransform {

  transform(rating: number): string {
    return rating.toFixed(2) + ' / 5';
  }

}
