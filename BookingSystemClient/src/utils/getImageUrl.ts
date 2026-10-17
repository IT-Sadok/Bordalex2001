const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

export default function getImageUrl(imagePath: string | null): string | null {
  if (!imagePath) return null;

  if (/^https?:\/\//i.test(imagePath)) return imagePath;

  return `${API_BASE_URL}/${imagePath}`;
}
