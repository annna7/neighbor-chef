import { Directive, ElementRef, Renderer2, HostListener } from '@angular/core';
import { animate, style, transition, trigger, AnimationBuilder, AnimationPlayer } from '@angular/animations';

@Directive({
  selector: '[appImageLoader]'
})
export class ImageLoaderDirective {
  private player !: AnimationPlayer;

  constructor(private el: ElementRef, private renderer: Renderer2, private builder: AnimationBuilder) {}

  @HostListener('load', ['$event.target'])
  onLoad(img: HTMLImageElement) {
    const animation = this.builder.build([
      style({ opacity: 0 }),
      animate(500, style({ opacity: 1 }))
    ]);

    this.player = animation.create(this.el.nativeElement);
    this.player.play();
  }

  @HostListener('error')
  onError(err: any) {
    console.error('Image failed to load.', err);
  }
}
