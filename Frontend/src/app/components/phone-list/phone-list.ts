import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Phone } from '../../models/phone.model';
import { PhoneService } from '../../services/phone.service';

@Component({
  selector: 'app-phone-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './phone-list.html',
  styleUrls: ['./phone-list.css'],
})
export class PhoneListComponent implements OnInit {
  phones: Phone[] = [];
  loading: boolean = true;
  error: string | null = null;

  constructor(private phoneService: PhoneService) {}

  ngOnInit(): void {
    this.loadPhones();
  }

  loadPhones(): void {
    this.loading = true;
    this.phoneService.getPhones().subscribe({
      next: (phones) => {
        this.phones = phones;
        this.loading = false;
      },
      error: (err) => {
        this.error = err.message;
        this.loading = false;
        console.error('Error loading phones:', err);
      },
    });
  }

  deletePhone(id: number): void {
    if (confirm('Are you sure you want to delete this phone?')) {
      this.phoneService.deletePhone(id).subscribe({
        next: () => {
          this.phones = this.phones.filter((phone) => phone.id !== id);
        },
        error: (err) => {
          console.error('Error deleting phone:', err);
          alert('Failed to delete phone: ' + err.message);
        },
      });
    }
  }
}
