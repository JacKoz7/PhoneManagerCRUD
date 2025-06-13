import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  template: `
    <div class="app-container">
      <header>
        <div class="header-content">
          <h1>Mobile Phone Management</h1>
        </div>
      </header>
      <main>
        <router-outlet></router-outlet>
      </main>
      <footer>
        <p>&copy; 2025 Mobile Phone Management System</p>
      </footer>
    </div>
  `,
  styles: [`
    .app-container {
      min-height: 100vh;
      display: flex;
      flex-direction: column;
      background-color: #f5f7fa;
    }
    
    header {
      background-color: #3f51b5;
      color: white;
      box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    }
    
    .header-content {
      max-width: 1200px;
      margin: 0 auto;
      padding: 1rem 20px;
    }
    
    h1 {
      margin: 0;
      font-size: 1.5rem;
    }
    
    main {
      flex: 1;
    }
    
    footer {
      background-color: #3f51b5;
      color: white;
      text-align: center;
      padding: 1rem;
      margin-top: auto;
    }
  `]
})
export class AppComponent {
  title = 'Mobile Phone Management';
}