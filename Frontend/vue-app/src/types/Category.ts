export interface Category {
  /**
   * Identifiant unique de la catégorie (GUID)
   */
  id: string;

  /**
   * Nom de la catégorie
   */
  name: string;

  /**
   * Emoji représentant la catégorie
   * @example '📦', '🛒', '🥑', '🥖', '🥩', '🥛', '🍎', '🍅', '🥕', '🧀'
   */
  icon: string;

  /**
   * Classe CSS du dégradé de couleur
   * @example 'from-blue-400 to-blue-600'
   */
  gradient: string;
} 