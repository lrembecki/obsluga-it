# Business Plan

## Company Information

| Brand             | Area              |
|-------------------|-------------------|
| Rembecki Studio   | Parent Company    |
| Dotyk Piękna      | Nail Styling      |
| Rembecki Digital  | Software / Web    |

---

# 1. Document Purpose

A working document describing the concept of establishing a sole proprietorship covering two main areas:

1. Nail styling services (run by wife).
2. Website development and technology services.

The purpose of this document is to create a plan for building a custom business management platform that will handle:

- Finances
- Invoicing
- Inventory
- Budget
- Websites
- Clients purchasing websites on a subscription model

This document will be developed iteratively before implementation begins.

---

# 2. Business Structure

## Area 1 — Beauty (Nail Styling)

**Scope:**

- Providing nail styling services
- Client management
- Appointment scheduling
- Product inventory (polishes, drill bits, files, etc.)
- Portfolio publication

**Website:**

- Separate domain
- Work gallery
- Price list
- Contact
- Optional appointment booking

## Area 2 — Website Development

**Scope:**

- Building websites for clients
- Selling websites on a subscription model
- Hosting and maintenance
- Client access to a management panel

**Website:**

- Separate domain
- Service offering
- Portfolio
- Contact form
- Order system

---

# 3. Custom Administrative Platform

A central platform for business management.

**Assumption:** the platform will be developed as a SaaS product in the future.

The platform will handle:

- Financial management
- Invoicing
- Inventory
- Client management
- Client website management
- CMS
- Subscriptions

---

# 4. Platform Architecture

## Approach

Modular monolith architecture.

**Reasons:**

- Easier maintenance
- Faster development
- Easy module extraction in the future

---

# 5. Core System Modules

## 5.1 Finances

**Features:**

- Revenue tracking
- Expense tracking
- Monthly reports
- Annual reports
- Income tax calculation
- Profit analysis

---

## 5.2 Invoices

**Features:**

- Invoice generation
- PDF export
- Invoice history
- Automatic numbering

---

## 5.3 Inventory

**Features:**

- Product list
- Stock levels
- Product consumption tracking
- Low stock alerts

---

## 5.4 CRM

**Features:**

- Client database
- Order history
- Notes

---

## 5.5 CMS

**Features:**

- Website content editing
- Section management
- Blog

---

## 5.6 Client Website Management

**Features:**

- Client list
- Domains
- Hosting
- Subscription status

---

# 6. SaaS Model for Clients

The platform will enable selling the admin panel to external clients.

**Client receives:**

- CMS panel
- Website content management
- Basic analytics

**Model:**

- Monthly subscription

---

# 7. MVP (Minimum Viable Product)

The first version should include:

1. CMS
2. Invoices
3. Finances
4. CRM
5. Basic Inventory

---

# 8. Development Stages

**Stage 1:** Platform for own business operations

**Stage 2:** Multi-tenant architecture

**Stage 3:** SaaS for external clients

---

# 9. Risks

- Excessive project scope creep
- Too broad MVP scope
- Lack of time for development

---

# 10. Implementation Strategy

**Principles:**

- Iterative development
- MVP first
- Automation
- Maximum use of own competencies
