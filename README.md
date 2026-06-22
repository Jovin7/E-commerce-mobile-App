E-Commerce Product Catalog

A mobile-optimized Unity application designed to display a large product catalog using highly efficient, virtualized scrolling, advanced multi-select filtering, and a dedicated 3D product viewer.

This project was built focusing on performance, scalability, and clean architecture principles.

🚀 Features

📱 Product Catalog & UI

Responsive Grid Layout: Automatically adapts layout based on device orientation.

Portrait: 2 columns

Landscape: 4 columns

Details Panel: Instant view of name, category, subcategory, detailed description, and thumbnail.

State Persistence: Filter selection and scroll state are preserved when returning to the catalog from other scenes.

⚡ Performance-Oriented Virtualization

UI Virtualization (Cell Recycling): Instantiates only the visible product UI cells. Instead of instantiating 1,000+ GameObjects, cells are dynamically pooled and recycled during scroll.

Constant Memory Footprint: Keeps memory consumption low and stable, maintaining a locked 60 FPS on modern mobile devices.

🔍 Advanced Product Filtering

Multi-Select Filters: Filter seamlessly across categories and subcategories simultaneously.

Instant Application: Filter evaluation is performed instantly against cached data and immediately updates the virtualized scroll view.

🕶️ 3D Product Viewer

Dedicated 3D Scene: Secure state preservation allows transition into a 3D orbit viewer and back.

Interactive Controls: Orbit camera rotation (drag) and intuitive pinch-to-zoom support.

On-Demand Loading: Loads the respective 3D model asset dynamically based on the selected catalog item.

🏗️ Architecture Highlights

The codebase is built around modern clean architecture principles, ensuring high maintainability and low coupling:

Data-Driven design (M-V-P / Separation of Concerns): Decoupled Data layer (JSON parser/repository), Presentation layer (UI controllers), and View/Rendering layer (3D Orbit scene).

State Management: Filter states, scroll positions, and selected items are stored in dedicated persistent state models, allowing seamless back-navigation.

Dynamic Recycling System: Custom virtualized scrolling algorithm managing lifecycle events for UI cell elements.

📄 Data Format

The application parses catalog data dynamically from a JSON configuration file.

📁 File Path: Assets/StreamingAssets/products.json

JSON Schema Example

[
  {
    "id": "3",
    "name": "Nike Air Max",
    "category": "Clothes",
    "subCategory": "Male",
    "description": "Comfortable running shoes for daily use.",
    "thumbnailURL": "shoe.png",
    "modelCategory": "Shoe"
  }
]


⚙️ Requirements & Dependencies

Requirement

Specification

Unity Engine

2022.3.62f3 (or newer LTS)

Platforms

Mobile (Android / iOS)

UI Framework

Unity UI (UGUI) + TextMeshPro

Tweening Engine

LeanTween (used for smooth UI animations & transitions)

🛠️ Testing & Performance Considerations

High Dataset Scalability: The project includes a pre-populated dataset inside StreamingAssets/products.json containing 1,000+ items to allow reviewers to stress-test the UI virtualization.

Filtered Scroll Recycler: Applying filters does not destroy and recreate UI cells; it re-binds active data arrays directly onto the active pooled objects, keeping garbage collection spikes to zero.

👤 Author

Jovin Finny

Unity Developer | XR Developer

💼 4.5+ Years of Industry Experience