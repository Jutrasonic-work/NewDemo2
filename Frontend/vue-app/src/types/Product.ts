/**
 * Unités de mesure disponibles pour les produits
 */
export enum ProductUnit {
  PIECE = 'piece',
  KILOGRAM = 'kg',
  GRAM = 'g',
  LITER = 'L',
  MILLILITER = 'mL',
  PACK = 'pack',
  BOX = 'boîte'
}

export interface Product {
  /**
   * Identifiant unique du produit (GUID)
   */
  id: string;

  /**
   * Nom du produit
   */
  name: string;

  /**
   * Description détaillée du produit
   */
  description: string;

  /**
   * Prix du produit en euros
   */
  price: number;

  /**
   * URL de l'image du produit
   * Si non définie, une image par défaut sera utilisée
   */
  image?: string;

  /**
   * Identifiant de la catégorie (GUID)
   * Référence à Category.id
   */
  category: string;

  /**
   * Quantité en stock
   */
  stock: number;

  /**
   * Unité de mesure du produit
   */
  unit: ProductUnit;

  /**
   * Indique si le produit est disponible à la vente
   */
  inStock: boolean;

  /**
   * Date de création du produit
   */
  createdAt?: Date;

  /**
   * Date de dernière modification du produit
   */
  updatedAt?: Date;
} 