const distributors = [
    "https://localhost:44307/api/Product", // TechWorld Port
    "https://localhost:7030/api/Product", // ElectroCom Port
    "https://localhost:7276/api/Product"  // GadgetCentral Port
];

let cart = [];
window.onload = async () => {
    await fetchAllProducts();
};

async function fetchAllProducts() {
    const productGrid = document.querySelector('.product-grid');
    const status = document.getElementById('statusMessage');

    status.innerText = "Connecting to distributors...";
    productGrid.innerHTML = "";

    try {

        const requests = distributors.map(url => fetch(url).then(res => res.json()));
        const allResults = await Promise.all(requests);

        const flatList = allResults.flat();

        const uniqueProductsMap = new Map();

        flatList.forEach(product => {
             uniqueProductsMap.set(product.productId, product);
        });


        const uniqueList = Array.from(uniqueProductsMap.values());

        uniqueList.forEach(product => {
            displayProductCard(product);
        });

        status.innerText = "";
    } catch (err) {
        console.error("Fetch Error:", err);
        status.innerText = "Error: Check if all 3 Distributor APIs are running.";
    }
}


function displayProductCard(product) {
    const grid = document.querySelector('.product-grid');

    let image = "itemName.jpg"

    grid.innerHTML += `
        <div class="product-card">
            <img src="${image}" alt="${product.productName}">
            <div class="card-info">
                <h3>${product.productName}</h3>
                <p class="product-code">ID: ${product.productId}</p>
                <button class="add-btn" onclick="addToCart('${product.productId}', '${product.productName}')">
                    Add to Cart 🛒
                </button>
            </div>
        </div>
    `;
}

async function addToCart(pId, pName) {
    const existing = cart.find(item => item.productId === pId);
    if (existing) {
        existing.quantity += 1;
    } else {
        cart.push({
            productId: pId,
            name: pName,
            quantity: 1,
            finalPrice: 0,
            distributor: "",
            delivery: 0
        });
    }
    await updatePricing();
}

async function updatePricing() {
    for (let item of cart) {
        try {
            const response = await fetch('/api/search', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ productId: item.productId, quantity: item.quantity })
            });
            if (response.ok) {
                const data = await response.json();
                item.finalPrice = data.finalTotalPrice;
                item.distributor = data.bestDistributor;
                item.delivery = data.deliveryDays;
            }
        } catch (err) { console.error("Search API Error:", err); }
    }
    renderUI();
}

function renderUI() {
    const list = document.getElementById('cartItems');
    const summary = document.getElementById('cartSummary');

    if (cart.length === 0) {
        list.innerHTML = '<p class="empty-msg">Your basket is empty.</p>';
        summary.style.display = 'none';
        return;
    }

    summary.style.display = 'block';
    list.innerHTML = '';
    let total = 0;

    cart.forEach(item => {
        total += item.finalPrice;
        list.innerHTML += `
            <div class="cart-item">
                <div>
                    <strong>${item.name}</strong><br>
                </div>
                <div class="qty-controls">
                    <button class="qty-btn" onclick="reduceQty('${item.productId}')">➖</button>
                    <span>${item.quantity}</span>
                    <button class="qty-btn" onclick="addToCart('${item.productId}', '${item.name}')">➕</button>
                </div>
                <div>$${item.finalPrice.toFixed(2)}</div>
            </div>`;
    });

    document.getElementById('grandTotal').innerText = `$${total.toFixed(2)}`;
}


async function reduceQty(pId) {
    const index = cart.findIndex(item => item.productId === pId);
    if (index === -1) return;

    cart[index].quantity -= 1;

    if (cart[index].quantity <= 0) {
        cart.splice(index, 1);
    }

    await updatePricing();
}


async function placeFinalOrder() {
    const status = document.getElementById('statusMessage');
    status.innerText = "Processing order with distributors...";

    try {
        for (const item of cart) {
            await fetch('/api/order', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    distributor: item.distributor,
                    productId: item.productId,
                    quantity: item.quantity
                })
            });
        }
        alert("Success! Your orders were placed and stock was updated.");
        cart = [];
        renderUI();
        status.innerText = "";
    } catch (err) {
        status.innerText = "Checkout failed. Please try again.";
        console.error("Order Error:", err);
    }
}