import React from 'react';
import { Card, CardMedia, CardContent, Typography, Button, Box, Chip, Rating } from '@mui/material';
import { ShoppingCart, Favorite, Star } from '@mui/icons-material';

type ProductCardProps = {
  product: {
    id: number;
    name: string;
    price: number;
    imageUrl: string;
    category: string;
    isInfluencerChoice: boolean;
    influencerComment?: string;
    rating: number;
    reviewCount: number;
  };
  onAddToCart?: (product: any) => void;
};

const ProductCard: React.FC<ProductCardProps> = ({ product, onAddToCart }) => {
  return (
    <Card sx={{ 
      maxWidth: 320, 
      borderRadius: 3,
      boxShadow: '0 4px 20px rgba(255, 107, 53, 0.1)',
      transition: 'all 0.3s cubic-bezier(0.4, 0, 0.2, 1)',
      position: 'relative',
      overflow: 'visible',
      '&:hover': {
        transform: 'translateY(-8px)',
        boxShadow: '0 8px 32px rgba(255, 107, 53, 0.25)'
      }
    }}>
      {product.isInfluencerChoice && (
        <Chip 
          label="⭐ Influencer Seçimi" 
          sx={{ 
            position: 'absolute', 
            top: 12, 
            right: 12, 
            zIndex: 1,
            bgcolor: 'linear-gradient(45deg, #FF6B35, #FFA500)',
            background: 'linear-gradient(45deg, #FF6B35, #FFA500)',
            color: 'white',
            fontWeight: 600,
            fontSize: '0.75rem'
          }} 
        />
      )}
      
      <CardMedia
        component="img"
        height="220"
        image={product.imageUrl || '/images/products/default.jpg'}
        alt={product.name}
        sx={{ 
          objectFit: 'cover',
          transition: 'transform 0.3s',
          '&:hover': {
            transform: 'scale(1.05)'
          }
        }}
      />
      
      <CardContent sx={{ p: 3 }}>
        <Typography gutterBottom variant="h6" component="div" sx={{ 
          fontWeight: 600,
          height: '3rem',
          overflow: 'hidden',
          display: '-webkit-box',
          WebkitLineClamp: 2,
          WebkitBoxOrient: 'vertical'
        }}>
          {product.name}
        </Typography>
        
        <Box sx={{ display: 'flex', alignItems: 'center', mb: 1 }}>
          <Rating 
            value={product.rating} 
            readOnly 
            size="small" 
            sx={{ color: '#FF6B35' }}
          />
          <Typography variant="body2" color="text.secondary" sx={{ ml: 1 }}>
            ({product.reviewCount})
          </Typography>
        </Box>
        

        
        <Typography variant="body2" color="text.secondary" sx={{ 
          mb: 2,
          bgcolor: '#F8F9FA',
          px: 1,
          py: 0.5,
          borderRadius: 1,
          display: 'inline-block'
        }}>
          {product.category}
        </Typography>
        
        <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mt: 2 }}>
          <Typography variant="h5" sx={{ 
            color: '#FF6B35',
            fontWeight: 700
          }}>
            ₺{product.price.toFixed(2)}
          </Typography>
          
          <Box sx={{ display: 'flex', gap: 1 }}>
            <Button 
              size="small" 
              sx={{ 
                minWidth: 0, 
                p: 1,
                borderRadius: 2,
                '&:hover': {
                  bgcolor: '#FFE5E5'
                }
              }}
            >
              <Favorite sx={{ color: '#FF6B35' }} />
            </Button>
            <Button 
              variant="contained" 
              size="small" 
              startIcon={<ShoppingCart />}
              onClick={() => onAddToCart?.(product)}
              sx={{ 
                bgcolor: '#FF6B35',
                color: 'white',
                fontWeight: 600,
                px: 2,
                py: 1,
                borderRadius: 2,
                '&:hover': { 
                  bgcolor: '#E55A2B',
                  transform: 'scale(1.05)'
                }
              }}
            >
              Sepet
            </Button>
          </Box>
        </Box>
      </CardContent>
    </Card>
  );
};

export default ProductCard;
