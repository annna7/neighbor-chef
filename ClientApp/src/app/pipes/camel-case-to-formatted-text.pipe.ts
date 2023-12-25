import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'camelCaseToFormattedText'
})

export class CamelCaseToFormattedTextPipe implements PipeTransform {

  transform(value: string, ...args: unknown[]): string {
    // Replace capital letters with space + capital letter and make the first letter uppercase
    return value
      .replace(/([A-Z])/g, ' $1') // inserts a space before capital letters
      .replace(/^./, function(str) { return str.toUpperCase(); }) // converts first character to uppercase
      .trim(); // Removes any leading space due to capital letter at the beginning
  }

}
