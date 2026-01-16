//const apiUri = '/api/products';
const apiUri = '/api/product';

// 1. Function to Load Products (GET)
async function loadProducts() {
    try {
        const response = await fetch(apiUri); // Call GET /api/products
        const data = await response.json();

        const tableBody = document.getElementById('productTableBody');
        tableBody.innerHTML = ''; // Clear table first

        data.forEach(p => {
            tableBody.innerHTML += `
                <tr>
                    <td>${p.productId}</td>
                    <td>${p.productName}</td>
                    <td>${p.pricePerUnit}</td>
                    <td>${p.availableQuantity}</td>
                    <td>${p.deliveryDays}</td>
                </tr>`;
        });
    } catch (error) {
        console.error('Error loading data:', error);
    }
}

// 2. Function to Add Product (POST)
async function addProduct() {
    const Product = {
        productId: document.getElementById('pId').value,
        productName: document.getElementById('pName').value,
        pricePerUnit: parseFloat(document.getElementById('pPrice').value),
        availableQuantity: parseInt(document.getElementById('pQty').value),
        deliveryDays: parseInt(document.getElementById('pDays').value)
    };

    const response = await fetch(apiUri, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(Product) // Convert C# object to JSON string
    });

    if (response.ok) {
        document.getElementById('message').innerText = "✅ Product Added!";
        loadProducts(); // Refresh the table
    } else {
        document.getElementById('message').innerText = "❌ Error adding product.";
    }
}

// Start by loading data
loadProducts();