document.addEventListener("DOMContentLoaded", function () {
    const projectId = document.getElementById("project-id").value;
    const commentList = document.getElementById("comment-list");
    const commentForm = document.getElementById("comment-form");

    function loadComments() {
        fetch(`/api/ProjectComment/${projectId}`)
            .then(response => response.json())
            .then(data => {
                commentList.innerHTML = "";
                data.forEach(comment => {
                    const div = document.createElement("div");
                    div.textContent = `${comment.content} (${new Date(comment.createdDate).toLocaleString()})`;
                    commentList.appendChild(div);
                });
            });
    }

    commentForm.addEventListener("submit", function (e) {
        e.preventDefault();
        const content = document.getElementById("comment-content").value;

        fetch("/api/ProjectComment", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                content: content,
                projectId: projectId
            })
        })
            .then(response => response.json())
            .then(() => {
                document.getElementById("comment-content").value = "";
                loadComments();
            });
    });

    loadComments();
});
