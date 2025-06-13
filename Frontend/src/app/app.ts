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
          <h1>Phone Manager</h1>
          <p class="tagline">Manage your mobile inventory with ease</p>
        </div>
      </header>
      <main>
        <router-outlet></router-outlet>
      </main>
      <footer>
        <div class="footer-content">
          <p>&copy; 2025 PhoneVault Management System</p>
          <div class="footer-links">
            <a href="#">About</a>
            <a href="#">Privacy</a>
            <a href="#">Terms</a>
          </div>
        </div>
      </footer>
    </div>
  `,
  styles: [`
    .app-container {
      min-height: 100vh;
      display: flex;
      flex-direction: column;
    }
    
    header {
      background: linear-gradient(135deg, var(--primary-color), var(--primary-dark));
      color: white;
      box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
      padding: 1.5rem 0;
    }
    
    .header-content {
      max-width: 1200px;
      margin: 0 auto;
      padding: 0 20px;
      display: flex;
      flex-direction: column;
      align-items: center;
    }
    
    h1 {
      margin: 0;
      font-size: 2.2rem;
      letter-spacing: 1px;
    }
    
    .tagline {
      margin-top: 0.5rem;
      font-size: 1rem;
      opacity: 0.9;
    }
    
    main {
      flex: 1;
      padding: 2rem 0;
    }
    
    footer {
      background-color: #333;
      color: white;
      padding: 1.5rem 0;
      margin-top: auto;
    }
    
    .footer-content {
      max-width: 1200px;
      margin: 0 auto;
      padding: 0 20px;
      display: flex;
      justify-content: space-between;
      align-items: center;
    }
    
    .footer-links {
      display: flex;
      gap: 20px;
    }
    
    .footer-links a {
      color: white;
      opacity: 0.8;
    }
    
    .footer-links a:hover {
      opacity: 1;
    }
    
    @media (max-width: 768px) {
      .footer-content {
        flex-direction: column;
        gap: 15px;
      }
    }
  `]
})
export class AppComponent {
  title = 'PhoneVault';
}