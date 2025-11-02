import { Component } from '@angular/core';

@Component({
  selector: `app-dialog-container`,
  template: `
    @for (dialog of components; track dialog) {
      <div class="backdrop">
        <div class="modal">test</div>
      </div>
    }
  `,
  styles: `
    .backdrop {
      background-color: rgba(0, 0, 0, 0.7);
      position: absolute;
      top: 0;
      left: 0;
      right: 0;
      bottom: 0;
      z-index: 1;
      display: flex;
      flex-direction: column;
      justify-content: start;
    }

    .modal {
      display: flex;
      flex-direction: column;
      padding: 0.5rem 1rem;
      border: black;
      border-radius: 0.15rem;
      margin: auto;
      margin-top: 2rem;
      min-width: 25rem;
      background-color: rgba(255, 255, 255, 0.05);
    }
  `,
})
export class DialogContainer {
  protected readonly components: any[] = [{}];
}
