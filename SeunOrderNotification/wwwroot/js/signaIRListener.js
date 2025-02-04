// Update notification count in real-time
let connection = new signalR.HubConnectionBuilder()
    .withUrl("/notificationhub")
    .build();

connection.start().catch(err => console.error(err));


function loadOrders() {
    fetch('/Dashboard/Orders/GetUserOrders')
        .then(response => response.json())
        .then(data => {
            console.log(data)
            const orderCardsContainer = document.querySelector('.dashboard-order-list');
            orderCardsContainer.innerHTML = '';

            data.forEach(order => {
                const orderCardHtml = `
                        <div class="col-lg-4 col-md-6 mb-4">
                            <div class="order-card card mb-4">
                                <div class="card-header">
                                    <h5>${order.customerName}</h5>
                                    <span class="order-date">${new Date(order.sessionDateTime).toLocaleString()}</span>
                                </div>
                                <div class="card-body">
                                    <p><i class="fas fa-clock"></i> ${new Date(order.sessionDateTime).toLocaleTimeString()}</p>
                                    <p><i class="fas fa-hourglass-half"></i> ${order.sessionLengthMinutes} minutes</p>
                                    <p><i class="fas fa-envelope"></i> ${order.customerEmail}</p>
                                    <p><i class="fas fa-phone"></i> ${order.customerPhone}</p>
                                </div>
                            </div>
                        </div>
                    `;
                orderCardsContainer.insertAdjacentHTML('beforeend', orderCardHtml);
            });
        });
}

function loadNotificationsCount() {
    fetch('/Dashboard/GetNotifications')
        .then(response => response.json())
        .then(data => {
            document.getElementById('notificationCount').textContent = data.length;
        });
}

function loadNotifications() {
    fetch('/Dashboard/GetNotifications')
        .then(response => response.json())
        .then(data => {
            const notificationList = document.querySelector('.notification-list');
            notificationList.innerHTML = '';

            data.forEach(notification => {
                const notificationItem = document.createElement('div');
                notificationItem.className = `notification-item ${notification.isRead ? '' : 'unread'}`;
                notificationItem.dataset.notificationId = notification.id;
                notificationItem.innerHTML = `
                        <div class="notification-content">
                            <h6>New Booking Request</h6>
                            <p>From: ${notification.order.customerName}</p>
                            <p>Date: ${new Date(notification.order.sessionDateTime).toLocaleString()}</p>
                            <p>Duration: ${notification.order.sessionLengthMinutes} minutes</p>
                        </div>
                        <div class="notification-time">
                            ${new Date(notification.createdAt).toLocaleString()}
                        </div>
                    `;
                notificationList.appendChild(notificationItem);
            });
        });
}