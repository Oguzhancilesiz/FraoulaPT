import React, { useState } from 'react';
import { 
  Container, 
  Grid, 
  Typography, 
  Box, 
  TextField, 
  Select, 
  MenuItem, 
  FormControl, 
  InputLabel,
  Chip,
  Button,
  Paper
} from '@mui/material';
import ProductCard from '../components/ProductCard';
import { 
  FitnessCenter, 
  LocalOffer, 
  Whatshot, 
  Checkroom,
  Search,
  FilterList
} from '@mui/icons-material';

const Marketplace = () => {
  const [searchTerm, setSearchTerm] = useState('');
  const [selectedCategory, setSelectedCategory] = useState('');
  const [sortBy, setSortBy] = useState('');

  // Örnek ürünler - influencer temalı fitness ürünleri
  const products = [
    {
      id: 1,
      name: 'FraoulaPT Signature Tişört',
      price: 199.99,
      imageUrl: '/images/products/tshirt1.jpg',
      category: 'Giyim',
      isInfluencerChoice: true,
      rating: 4.9,
      reviewCount: 127
    },
    {
      id: 2,
      name: 'Premium Whey Protein 2kg',
      price: 299.99,
      imageUrl: '/images/products/whey.jpg',
      category: 'Supplement',
      isInfluencerChoice: true,
      rating: 4.8,
      reviewCount: 89
    },
    {
      id: 3,
      name: 'Pro Gym Eldivenleri',
      price: 149.99,
      imageUrl: '/images/products/gloves.jpg',
      category: 'Aksesuar',
      isInfluencerChoice: false,
      rating: 4.6,
      reviewCount: 45
    },
    {
      id: 4,
      name: 'FraoulaPT Hoodie',
      price: 349.99,
      imageUrl: '/images/products/hoodie.jpg',
      category: 'Giyim',
      isInfluencerChoice: true,
      rating: 4.9,
      reviewCount: 203
    },
    {
      id: 5,
      name: 'BCAA Energy Drink',
      price: 89.99,
      imageUrl: '/images/products/bcaa.jpg',
      category: 'Supplement',
      isInfluencerChoice: false,
      rating: 4.4,
      reviewCount: 67
    },
    {
      id: 6,
      name: 'Resistance Bands Set',
      price: 179.99,
      imageUrl: '/images/products/bands.jpg',
      category: 'Ekipman',
      isInfluencerChoice: true,
      influencerComment: 'Evde antrenman için harika! 🏠',
      rating: 4.7,
      reviewCount: 156
    }
  ];

  const categories = [
    { value: 'Giyim', icon: <Checkroom />, color: '#FF6B35' },
    { value: 'Supplement', icon: <LocalOffer />, color: '#FFA500' },
    { value: 'Aksesuar', icon: <FitnessCenter />, color: '#FF8C42' },
    { value: 'Ekipman', icon: <Whatshot />, color: '#E55A2B' }
  ];

  const filteredProducts = products.filter(product => {
    const matchesSearch = product.name.toLowerCase().includes(searchTerm.toLowerCase());
    const matchesCategory = selectedCategory === '' || product.category === selectedCategory;
    return matchesSearch && matchesCategory;
  });

  const handleAddToCart = (product: any) => {
    // Sepet mantığı burada olacak
    console.log('Sepete eklendi:', product);
  };

  return (
    <Container maxWidth="xl" sx={{ py: 4 }}>
      {/* Hero Section */}
      <Box sx={{ 
        mb: 6, 
        textAlign: 'center',
        background: 'linear-gradient(135deg, #FF6B35 0%, #FFA500 100%)',
        color: 'white',
        borderRadius: 4,
        p: 6,
        position: 'relative',
        overflow: 'hidden'
      }}>
        <Typography variant="h2" sx={{ 
          fontWeight: 800, 
          mb: 2,
          textShadow: '2px 2px 4px rgba(0,0,0,0.3)'
        }}>
          FraoulaPT Market 🛍️
        </Typography>
        <Typography variant="h5" sx={{ 
          mb: 3,
          opacity: 0.9,
          fontWeight: 300
        }}>
          Influencer onaylı premium fitness ürünleri
        </Typography>
        <Typography variant="body1" sx={{ 
          maxWidth: 600,
          margin: '0 auto',
          opacity: 0.8
        }}>
          Fit yaşam tarzının en kaliteli ürünleri, özenle seçilmiş koleksiyonlar
        </Typography>
      </Box>
      
      {/* Kategori Chips */}
      <Box sx={{ mb: 4, display: 'flex', justifyContent: 'center', flexWrap: 'wrap', gap: 2 }}>
        <Chip
          label="Tümü"
          onClick={() => setSelectedCategory('')}
          variant={selectedCategory === '' ? 'filled' : 'outlined'}
          sx={{
            bgcolor: selectedCategory === '' ? '#FF6B35' : 'transparent',
            color: selectedCategory === '' ? 'white' : '#FF6B35',
            borderColor: '#FF6B35',
            fontWeight: 600,
            '&:hover': {
              bgcolor: selectedCategory === '' ? '#E55A2B' : '#FFE5E5'
            }
          }}
        />
        {categories.map((category) => (
          <Chip
            key={category.value}
            icon={category.icon}
            label={category.value}
            onClick={() => setSelectedCategory(category.value)}
            variant={selectedCategory === category.value ? 'filled' : 'outlined'}
            sx={{
              bgcolor: selectedCategory === category.value ? category.color : 'transparent',
              color: selectedCategory === category.value ? 'white' : category.color,
              borderColor: category.color,
              fontWeight: 600,
              '&:hover': {
                bgcolor: selectedCategory === category.value ? category.color : `${category.color}20`
              }
            }}
          />
        ))}
      </Box>
      
      {/* Filtreleme */}
      <Paper sx={{ 
        p: 3, 
        mb: 4,
        borderRadius: 3,
        boxShadow: '0 4px 20px rgba(0,0,0,0.08)'
      }}>
        <Box sx={{ 
          display: 'flex', 
          justifyContent: 'space-between', 
          alignItems: 'center',
          flexWrap: 'wrap',
          gap: 2
        }}>
          <Box sx={{ display: 'flex', alignItems: 'center', gap: 2, flexGrow: 1 }}>
            <Search sx={{ color: '#FF6B35' }} />
            <TextField 
              label="Ürün Ara" 
              variant="outlined" 
              size="small" 
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
              sx={{ 
                flexGrow: 1, 
                maxWidth: 400,
                '& .MuiOutlinedInput-root': {
                  '&.Mui-focused fieldset': {
                    borderColor: '#FF6B35'
                  }
                }
              }}
            />
          </Box>
          
          <Box sx={{ display: 'flex', gap: 2, alignItems: 'center' }}>
            <FilterList sx={{ color: '#FF6B35' }} />
            <FormControl size="small" sx={{ minWidth: 140 }}>
              <InputLabel>Sırala</InputLabel>
              <Select 
                label="Sırala" 
                value={sortBy}
                onChange={(e) => setSortBy(e.target.value)}
              >
                <MenuItem value="">Varsayılan</MenuItem>
                <MenuItem value="price-low">Fiyat: Düşük-Yüksek</MenuItem>
                <MenuItem value="price-high">Fiyat: Yüksek-Düşük</MenuItem>
                <MenuItem value="rating">En Popüler</MenuItem>
                <MenuItem value="newest">En Yeni</MenuItem>
              </Select>
            </FormControl>
          </Box>
        </Box>
      </Paper>

      {/* Influencer Öne Çıkanlar */}
      <Box sx={{ mb: 4 }}>
        <Typography variant="h4" sx={{ 
          mb: 3, 
          fontWeight: 700,
          color: '#FF6B35',
          display: 'flex',
          alignItems: 'center',
          gap: 1
        }}>
          ⭐ Influencer Seçimleri
        </Typography>
        <Grid container spacing={3}>
          {filteredProducts
            .filter(product => product.isInfluencerChoice)
            .slice(0, 3)
            .map((product) => (
              <Grid xs={12} sm={6} lg={4} key={product.id}>
                <ProductCard product={product} onAddToCart={handleAddToCart} />
              </Grid>
            ))}
        </Grid>
      </Box>

      {/* Tüm Ürünler */}
      <Box>
        <Typography variant="h4" sx={{ 
          mb: 3, 
          fontWeight: 700,
          color: '#2C3E50'
        }}>
          Tüm Ürünler ({filteredProducts.length})
        </Typography>
        <Grid container spacing={3}>
          {filteredProducts.map((product) => (
            <Grid xs={12} sm={6} lg={4} xl={3} key={product.id}>
              <ProductCard product={product} onAddToCart={handleAddToCart} />
            </Grid>
          ))}
        </Grid>
      </Box>

      {filteredProducts.length === 0 && (
        <Box sx={{ 
          textAlign: 'center', 
          py: 8,
          color: 'text.secondary'
        }}>
          <Typography variant="h6">
            Aradığınız kriterlere uygun ürün bulunamadı 😔
          </Typography>
          <Typography variant="body1" sx={{ mt: 1 }}>
            Farklı anahtar kelimeler veya kategoriler deneyin
          </Typography>
        </Box>
      )}
    </Container>
  );
};

export default Marketplace;
