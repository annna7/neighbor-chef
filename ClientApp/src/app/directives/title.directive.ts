import {Directive, ElementRef, HostListener, Renderer2} from '@angular/core';

@Directive({
  selector: '[appTitle]',
})
export class TitleDirective {

  constructor(private el: ElementRef, private renderer: Renderer2) { }

  @HostListener('mouseenter') onMouseEnter() {
    this.renderer.setStyle(this.el.nativeElement, 'text-shadow', '5px 5px 10px rgba(0,0,0,0.5)');
    this.renderer.setStyle(this.el.nativeElement, 'transform', 'scale(1.02)');
  }

  @HostListener('mouseleave') onMouseLeave() {
    this.renderer.setStyle(this.el.nativeElement, 'box-shadow', '');
    this.renderer.setStyle(this.el.nativeElement, 'transform', '');
  }

  ngOnInit() {
    this.renderer.setStyle(this.el.nativeElement, 'font-family', 'Satisfy');
    this.renderer.setStyle(this.el.nativeElement, 'font-size', '3em');
    this.renderer.setStyle(this.el.nativeElement, 'color', '#dbaeae');
  }

}
