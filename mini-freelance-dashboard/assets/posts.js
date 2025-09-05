let posts = [];
let postsTable;
let tempId = -1; // for local posts

function openPostModal(post) {
  $('#postModalTitle').text(post ? 'Edit Post' : 'Add Post');
  $('#postForm [name=id]').val(post?.id || '');
  $('#postForm [name=title]').val(post?.title || '');
  $('#postForm [name=body]').val(post?.body || '');
  $('#postModal').removeClass('hidden');
}
function closePostModal(){ $('#postModal').addClass('hidden'); }

function openCommentsModal(){ $('#commentsModal').removeClass('hidden'); }
function closeCommentsModal(){ $('#commentsModal').addClass('hidden'); }

$(async function(){
  showLoader(true);
  try {
    posts = await fetchJSON('https://jsonplaceholder.typicode.com/posts');
    postsTable = $('#postsTable').DataTable({
      data: posts,
      columns: [
        { data: 'id' },
        { data: 'title' },
        { data: 'body' },
        { data: 'id', orderable:false, render: (id)=> `
           <div class="actions">
             <button class="btn btn-icon view-comments" data-id="${id}" title="Comments">💬</button>
             <button class="btn btn-icon edit" data-id="${id}" title="Edit">✏️</button>
             <button class="btn btn-icon delete" data-id="${id}" title="Delete">🗑️</button>
           </div>
        `}
      ]
    });
    toastr.success('Posts loaded');
  } catch (e) {
    console.error(e);
    toastr.error('Failed to load posts');
  } finally {
    showLoader(false);
  }

  $('#addPost').on('click', ()=> openPostModal(null));
  $('#closePostModal').on('click', closePostModal);
  $('#closeComments').on('click', closeCommentsModal);

  // Edit
  $('#postsTable tbody').on('click', 'button.edit', function(){
    const id = parseInt($(this).data('id'));
    const p = posts.find(x => x.id === id);
    if (!p) return;
    openPostModal(p);
  });

  // Delete (local)
  $('#postsTable tbody').on('click', 'button.delete', function(){
    const id = parseInt($(this).data('id'));
    const rowIdx = postsTable.rows().eq(0).filter(i => postsTable.row(i).data().id === id);
    postsTable.row(rowIdx).remove().draw(false);
    posts = posts.filter(p => p.id !== id);
    toastr.warning('Post deleted locally');
  });

  // View comments
  $('#postsTable tbody').on('click', 'button.view-comments', async function(){
    const id = parseInt($(this).data('id'));
    showLoader(true);
    try {
      const comments = await fetchJSON(`https://jsonplaceholder.typicode.com/comments?postId=${id}`);
      const list = $('#commentsList').empty();
      comments.forEach(c => {
        list.append(`<li>
          <div><strong>${c.name}</strong></div>
          <div class="comment-email">${c.email}</div>
          <p>${c.body}</p>
        </li>`);
      });
      openCommentsModal();
      toastr.info('Loaded comments');
    } catch (e) {
      console.error(e);
      toastr.error('Failed to load comments');
    } finally {
      showLoader(false);
    }
  });

  // Save (add/edit) locally
  $('#postForm').on('submit', function(e){
    e.preventDefault();
    const form = Object.fromEntries(new FormData(this).entries());
    const id = form.id ? parseInt(form.id) : null;
    if (id) {
      // update
      const idx = posts.findIndex(p => p.id === id);
      if (idx >= 0) {
        posts[idx] = { ...posts[idx], title: form.title, body: form.body };
        const rowIdx = postsTable.rows().eq(0).filter(i => postsTable.row(i).data().id === id);
        postsTable.row(rowIdx).data(posts[idx]).draw(false);
        toastr.success('Post updated locally ✅');
      }
    } else {
      // add new local post
      const newPost = { id: tempId--, userId: 0, title: form.title, body: form.body };
      posts.unshift(newPost);
      postsTable.row.add(newPost).draw(false);
      toastr.success('Post added locally ✅');
    }
    closePostModal();
  });
});
