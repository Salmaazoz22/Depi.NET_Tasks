let users = [];
let table;

const FAV_KEY = 'favUsers';

function getFavs() {
  try { return JSON.parse(localStorage.getItem(FAV_KEY) || '[]'); } catch { return []; }
}
function setFavs(ids) {
  localStorage.setItem(FAV_KEY, JSON.stringify(ids));
}

function isFav(id) { return getFavs().includes(id); }
function toggleFav(id) {
  const favs = getFavs();
  const idx = favs.indexOf(id);
  if (idx >= 0) favs.splice(idx, 1); else favs.push(id);
  setFavs(favs);
  toastr.info(isFav(id) ? 'Added to favorites ⭐' : 'Removed from favorites');
}

function renderStar(id) {
  const filled = isFav(id) ? '★' : '☆';
  return `<button class="btn btn-icon star" data-id="${id}" title="Toggle Favorite">${filled}</button>`;
}

function openModal(user) {
  $('#modalTitle').text(user ? 'Edit User' : 'Add User');
  $('#userForm [name=id]').val(user?.id || '');
  $('#userForm [name=name]').val(user?.name || '');
  $('#userForm [name=email]').val(user?.email || '');
  $('#userForm [name=phone]').val(user?.phone || '');
  $('#userForm [name=company]').val(user?.company?.name || user?.company || '');
  $('#userModal').removeClass('hidden');
}
function closeModal() { $('#userModal').addClass('hidden'); }

$(async function(){
  showLoader(true);
  try {
    users = await fetchJSON('https://jsonplaceholder.typicode.com/users');
    // Flatten for DataTables
    const rows = users.map(u => ({
      id: u.id, name: u.name, email: u.email, phone: u.phone, company: u.company?.name ?? ''
    }));
    table = $('#usersTable').DataTable({
      data: rows,
      columns: [
        { data: 'id', render: (data)=> renderStar(data), orderable: false },
        { data: 'name' },
        { data: 'email' },
        { data: 'phone' },
        { data: 'company' },
        { data: 'id', orderable:false, render: (id)=> `
           <div class="actions">
             <button class="btn btn-icon view" data-id="${id}" title="View">👁️</button>
             <button class="btn btn-icon edit" data-id="${id}" title="Edit">✏️</button>
             <button class="btn btn-icon delete" data-id="${id}" title="Delete">🗑️</button>
           </div>
        `}
      ]
    });
    toastr.success('Users loaded');
  } catch (e) {
    console.error(e);
    toastr.error('Failed to load users');
  } finally {
    showLoader(false);
  }

  // click handlers
  $('#usersTable tbody').on('click', 'button.star', function(){
    const id = parseInt($(this).data('id'));
    toggleFav(id);
    // rerender cell
    const cell = table.cell($(this).closest('td'));
    cell.data(id).draw(false);
  });

  $('#usersTable tbody').on('click', 'button.view', function(){
    const id = parseInt($(this).data('id'));
    const u = users.find(x => x.id === id);
    if (!u) return;
    openModal({
      id: u.id, name: u.name, email: u.email, phone: u.phone, company: u.company?.name ?? ''
    });
    $('#userForm input, #userForm textarea').prop('disabled', true);
    $('#userForm .modal-actions').hide();
  });

  $('#usersTable tbody').on('click', 'button.edit', function(){
    const id = parseInt($(this).data('id'));
    const u = users.find(x => x.id === id);
    if (!u) return;
    openModal({
      id: u.id, name: u.name, email: u.email, phone: u.phone, company: u.company?.name ?? ''
    });
    $('#userForm input, #userForm textarea').prop('disabled', false);
    $('#userForm .modal-actions').show();
  });

  $('#usersTable tbody').on('click', 'button.delete', function(){
    const id = parseInt($(this).data('id'));
    // local-only delete
    const rowIdx = table.rows().eq(0).filter(idx => table.cell(idx, 1).data() && table.row(idx).data().id === id);
    table.row(rowIdx).remove().draw(false);
    users = users.filter(u => u.id !== id);
    toastr.warning('User deleted locally');
  });

  $('#closeModal').on('click', closeModal);

  $('#userForm').on('submit', function(e){
    e.preventDefault();
    const form = Object.fromEntries(new FormData(this).entries());
    const id = parseInt(form.id);
    const updated = { id, name: form.name, email: form.email, phone: form.phone, company: form.company };
    // update local list + table
    const idx = users.findIndex(u => u.id === id);
    if (idx >= 0) {
      users[idx] = { ...users[idx], ...updated, company: { name: form.company } };
      const rowIdx = table.rows().eq(0).filter(i => table.row(i).data().id === id);
      table.row(rowIdx).data({ id, name: updated.name, email: updated.email, phone: updated.phone, company: updated.company }).draw(false);
      toastr.success('User updated locally ✅');
    } else {
      // add new user
      const newId = Math.max(0, ...users.map(u => u.id)) + 1;
      const newUser = { id: newId, name: updated.name, email: updated.email, phone: updated.phone, company: { name: updated.company } };
      users.push(newUser);
      table.row.add({ id: newId, name: updated.name, email: updated.email, phone: updated.phone, company: updated.company }).draw(false);
      toastr.success('User added locally ✅');
    }
    closeModal();
  });
});
