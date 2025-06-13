import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Phone } from '../../models/phone.model';
import { PhoneService } from '../../services/phone.service';

@Component({
  selector: 'app-phone-form',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './phone-form.html',
  styleUrls: ['./phone-form.css']
})
export class PhoneFormComponent implements OnInit {
  phone: Phone = {
    name: '',
    manufacturer: '',
    phoneNumber: '',
    price: 0,
    inStock: true
  };
  
  isEditMode = false;
  loading = false;
  submitting = false;
  error: string | null = null;
  
  // Validation errors
  nameError: string | null = null;
  phoneNumberError: string | null = null;
  
  constructor(
    private phoneService: PhoneService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id && id !== 'new') {
      this.isEditMode = true;
      this.loadPhone(+id);
    }
  }

  loadPhone(id: number): void {
    this.loading = true;
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

  validateForm(): boolean {
    let isValid = true;
    
    // Validate name (max 15 characters)
    if (!this.phone.name) {
      this.nameError = 'Name is required';
      isValid = false;
    } else if (this.phone.name.length > 15) {
      this.nameError = 'Name cannot be longer than 15 characters';
      isValid = false;
    } else {
      this.nameError = null;
    }
    
    // Validate phone number (exactly 9 digits)
    const phoneNumberRegex = /^\d{9}$/;
    if (!this.phone.phoneNumber) {
      this.phoneNumberError = 'Phone number is required';
      isValid = false;
    } else if (!phoneNumberRegex.test(this.phone.phoneNumber)) {
      this.phoneNumberError = 'Phone number must be exactly 9 digits';
      isValid = false;
    } else {
      this.phoneNumberError = null;
    }
    
    return isValid;
  }

  savePhone(): void {
    if (!this.validateForm()) {
      return;
    }
    
    this.submitting = true;
    this.error = null;
    
    if (this.isEditMode) {
      this.phoneService.updatePhone(this.phone.id!, this.phone).subscribe({
        next: () => {
          this.router.navigate(['/phones']);
        },
        error: (err) => {
          this.submitting = false;
          this.error = err.message;
          console.error('Error updating phone:', err);
        }
      });
    } else {
      this.phoneService.createPhone(this.phone).subscribe({
        next: () => {
          this.router.navigate(['/phones']);
        },
        error: (err) => {
          this.submitting = false;
          this.error = err.message;
          console.error('Error creating phone:', err);
        }
      });
    }
  }
}