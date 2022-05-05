import json
from re import I

class Product:
    def __init__(self) -> None:
        self.name = ""
        self.image = ""
        self.description = ""
        self.price = 0
        self.discount = 0
    
    def to_json(self) -> dict:
        return {
            "Name": self.name,
            "Image": self.image,
            "Description": self.description,
            "Price": self.price,
            "Discount": self.discount
        }
        
processed_products = []

with open("Data/Raws/Product.json", "r") as file:
    
    products = json.load(file)
    for product in products:
        p = Product()
        p.name = product["name"]
        p.image = product["thumbnailImage"]
        p.description = product["description"]
        p.price = product["price"]
        p.discount = product["discount"]
        
        processed_products.append(p.to_json())

with open("Data/Processed/Product.json", "w") as file:
    json.dump(processed_products, file, indent=4, sort_keys=True)
