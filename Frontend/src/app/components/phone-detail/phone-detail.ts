import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Phone } from '../../models/phone.model';
import { PhoneService } from '../../services/phone.service';

@Component({
  selector: 'app-phone-detail',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './phone-detail.html',
  styleUrls: ['./phone-detail.css']
})
export class PhoneDetailComponent implements OnInit {
  phone: Phone | null = null;
  loading = true;
  error: string | null = null;

  constructor(
    private phoneService: PhoneService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadPhone(+id);
    }
  }

  loadPhone(id: number): void {
    this.phoneService.getPhone(id).subscribe({
      next: (phone) => {
        this.phone = phone;
        this.loading = false;
      },
      error: (err) => {
        this.error = err.message;
        this.loading = false;
        console.error('Error loading phone:', err);
      }
    });
  }

  deletePhone(): void {
    if (!this.phone || !this.phone.id) return;
    
    if (confirm('Are you sure you want to delete this phone?')) {
      this.phoneService.deletePhone(this.phone.id).subscribe({
        next: () => {
          this.router.navigate(['/phones']);
        },
        error: (err) => {
          console.error('Error deleting phone:', err);
          alert('Failed to delete phone: ' + err.message);
        }
      });
    }
  }
}