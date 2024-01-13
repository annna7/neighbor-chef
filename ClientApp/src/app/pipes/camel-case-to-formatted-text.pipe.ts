import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'camelCaseToFormattedText'
})

export class CamelCaseToFormattedTextPipe implements PipeTransform {

  transform(value: string, ...args: unknown[]): string {
    return value
      .replace(/([A-Z])/g, ' $1')
      .replace(/^./, function(str) { return str.toUpperCase(); })
      .trim();
  }

}
