const apiUrl = '/api/Product';

// 1. GET: Load products from the database
async function fetchProducts() {
    try {
        const response = await fetch(apiUrl);
        const products = await response.json();

        const tbody = document.getElementById('productTableBody');
        tbody.innerHTML = ''; // Clear table before filling

        products.forEach(p => {
            tbody.innerHTML += `
                <tr>
                    <td>${p.productId}</td>
                    <td>${p.productName}</td>
                    <td>$${p.pricePerUnit}</td>
                    <td>${p.availableQuantity}</td>
                    <td>${p.deliveryDays} Days</td>
                </tr>`;
        });
    } catch (err) {
        console.error("Error loading data:", err);
    }
}

// 2. POST: Send new product to the database
document.getElementById('productForm').addEventListener('submit', async (e) => {
    e.preventDefault(); // Stop the page from reloading

    const Products = {
        productId: document.getElementById('productId').value,
        productName: document.getElementById('productName').value,
        pricePerUnit: parseFloat(document.getElementById('price').value),
        availableQuantity: parseInt(document.getElementById('quantity').value),
        deliveryDays: parseInt(document.getElementById('days').value)
    };

    try {
        const response = await fetch(apiUrl, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(Products)
        });

        const status = document.getElementById('statusMessage');
        if (response.ok) {
            status.innerText = "Success: Product Added!";
            status.className = "success";
            document.getElementById('productForm').reset();
            fetchProducts(); // Refresh the list
        } else {
            status.innerText = "Error: Could not save.";
            status.className = "error";
        }
    } catch (err) {
        status.innerText = "Server Error.";
    }
});

// Start the page by loading existing data
fetchProducts();