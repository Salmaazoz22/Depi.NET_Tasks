# Mini Freelance Dashboard

A small, freelance-style project built with **HTML, CSS, Animate.css, JavaScript, jQuery, DataTables, Toastr**, and the **JSONPlaceholder API**.

## ✨ Features

- **Dashboard** with live statistics (Users / Posts / Comments).
- **Users** page:
  - DataTables grid with **view / edit / delete** (local) actions.
  - **⭐ Favorites** toggle stored in **LocalStorage**.
- **Posts** page:
  - Live search via DataTables.
  - **Add / Edit / Delete** posts **locally** (no server writes).
  - View **comments** for a post (fetched from API).
- **Toastr** notifications for every operation.
- **Loader** overlay during API calls.
- **Light/Dark Mode** persisted in LocalStorage.
- Subtle **Animate.css** transitions on the dashboard.

## 🛠️ Tech

- HTML, CSS
- JavaScript, jQuery
- DataTables
- Toastr
- Animate.css
- JSONPlaceholder APIs:
  - Users → https://jsonplaceholder.typicode.com/users
  - Posts → https://jsonplaceholder.typicode.com/posts
  - Comments → `https://jsonplaceholder.typicode.com/comments?postId={id}`

## 🚀 Run Locally

Just open `index.html` in a modern browser.

> Tip: For the best experience with `fetch`, use a local server (e.g., VS Code Live Server, `python -m http.server`, or any static server).

## 🗂️ Structure

```
.
├── index.html          # Dashboard (stats)
├── users.html          # Users DataTable + favorites
├── posts.html          # Posts with add/edit/delete + comments
└── assets
    ├── styles.css
    ├── app.js          # Shared helpers (loader, theme, fetch)
    ├── users.js
    └── posts.js
```

## 📌 Notes

- **Edits/Deletes/Adds** are local-only (in-memory) to simulate real UI behavior without a backend write.
- Favorites are saved to **LocalStorage** under the key `favUsers`.
- Theme preference is saved to **LocalStorage** under the key `theme`.

## 🧪 Tested On

- Chrome 120+
- Edge 120+

---

Made with ❤️ for the assignment.
