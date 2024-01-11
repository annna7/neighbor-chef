import {Directive, ElementRef, HostListener, Renderer2} from '@angular/core';

@Directive({
  selector: '[appSubtitle]',
})
export class SubtitleDirective {

  constructor(private el: ElementRef, private renderer: Renderer2) { }

  @HostListener('mouseenter') onMouseEnter() {
    this.renderer.setStyle(this.el.nativeElement, 'font-weight', 'bold');
    this.renderer.setStyle(this.el.nativeElement, 'color', '#e7e7e7');
  }

  @HostListener('mouseleave') onMouseLeave() {
    this.renderer.setStyle(this.el.nativeElement, 'font-weight', '');
    this.renderer.setStyle(this.el.nativeElement, 'color', 'white');
  }

  ngOnInit() {
    this.renderer.setStyle(this.el.nativeElement, 'font-family', 'Satisfy');
    this.renderer.setStyle(this.el.nativeElement, 'font-size', '2.5em');
    this.renderer.setStyle(this.el.nativeElement, 'color', 'white');
  }

}
