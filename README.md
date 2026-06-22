E-Commerce Product Catalog

A mobile-optimized Unity application that displays a large product catalog with efficient scrolling, filtering, and a dedicated 3D product viewer.

Features
Product Catalog
    Loads product data from JSON stored in StreamingAssets.
    Displays products in a responsive grid layout.
    Supports portrait and landscape orientations.
    Dynamic column count:
        Portrait: 2 columns
        Landscape: 4 columns
Virtualized Scrolling
    Uses UI virtualization (recycling cells) instead of instantiating all items.
    Maintains smooth performance with large datasets.
    Designed to handle 1,000+ products efficiently.
Product Filtering
    Filter by Category.
    Filter by Subcategory.
    Multi-select filter options.
    Apply and Reset functionality.
    Displays filtered results instantly.
Product Details
    Product information includes:
        Name
        Category
        Subcategory
        Description
        Thumbnail
3D Product Viewer
    Dedicated scene for viewing products in 3D.
    Orbit rotation around the model.
    Pinch-to-zoom support.
    Product model loading based on selected item.
    Back navigation preserves catalog state.
State Persistence
    Selected filters remain applied.
    Previously loaded catalog data is preserved.

Data Format

Example product entry:

{
      "id": "3",
      "name": "Nike Air Max",
      "category": "Clothes",
      "subCategory": "Male",
      "description": "Comfortable running shoes for daily use.",
      "thumbnailURL": "shoe.png",
      "modelCategory": "Shoe"
    },

Performance Considerations

Virtualized Grid

Instead of creating UI elements for every product:
    Only visible items are instantiated.
    Cells are recycled while scrolling.
    Memory usage remains constant.
    Suitable for catalogs containing thousands of products.
Filtering
    Uses cached product data.
    Updates visible content without recreating UI.
    Maintains virtualization after filtering.

Requirements
    Unity 2022.3.62f3
    Mobile Platform Support (Android/iOS)
    TextMeshPro
    LeanTween

Test Dataset

The project includes: StreamingAssets/products.json with sample product data.

The catalog is designed to support reviewer testing with datasets containing 1,000+ products.

Architecture Highlights
    Separation of Data, UI, and Viewer systems.
    Virtualized scrolling for performance.
    Filter state management using dedicated models.
    Scene-based 3D viewer architecture.
    Reusable and scalable UI components.

Author

Jovin Finny
Unity Developer | XR Developer
4.5+ Years Experience

This project was developed as part of a Unity technical assessment focusing on performance, scalability, and clean architecture.