const apiUrl = '/api/Product';

// --- 1. GET DATA (Fetch and Display) ---
async function loadProducts() {
    try {
        const response = await fetch(apiUrl);
        const data = await response.json();

        const tableBody = document.getElementById('tableBody');
        tableBody.innerHTML = ''; // Clear old rows

        data.forEach(p => {
            const row = `<tr>
                <td>${p.productId}</td>
                <td>${p.productName}</td>
                <td>$${p.pricePerUnit}</td>
                <td>${p.availableQuantity}</td>
                <td>${p.deliveryDays} Days</td>
            </tr>`;
            tableBody.innerHTML += row;
        });
    } catch (err) {
        console.error("Error loading products:", err);
    }
}

// --- 2. POST DATA (Send to API) ---
document.getElementById('productForm').addEventListener('submit', async (e) => {
    e.preventDefault(); // Prevent page from refreshing

    // Collect data from the text boxes
    const Product = {
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
            body: JSON.stringify(Product) // Turn the object into text
        });

        const status = document.getElementById('statusMsg');
        if (response.ok) {
            status.innerText = "Success: Product Added!";
            status.className = "success";
            document.getElementById('productForm').reset(); // Clear the form
            loadProducts(); // Refresh the table
        } else {
            status.innerText = "Error: Could not save product.";
            status.className = "error";
        }
    } catch (err) {
        console.error("Submit error:", err);
    }
});

// Run this when the page first opens
loadProducts();