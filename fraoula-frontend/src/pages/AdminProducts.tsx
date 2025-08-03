import React, { useState } from 'react';
import {
  Container,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Typography,
  Box,
  Chip,
  Button,
  IconButton,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  TextField,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  Grid,
  Card,
  CardContent,
  CardMedia,
  Switch,
  FormControlLabel,
  Fab
} from '@mui/material';
import {
  Add,
  Edit,
  Delete,
  Visibility,
  Star,
  Inventory,
  AttachMoney,
  Category
} from '@mui/icons-material';

const AdminProducts = () => {
  const [products, setProducts] = useState([
    {
      id: "1",
      name: 'FraoulaPT Signature Tişört',
      category: 'Giyim',
      price: 199.99,
      stock: 45,
      rating: 4.9,
      reviews: 127,
      isFeatured: true,
      isInfluencerChoice: true,
      image: '/images/products/tshirt1.jpg',
      status: 'Aktif'
    },
    {
      id: "2",
      name: 'Premium Whey Protein 2kg',
      category: 'Supplement',
      price: 299.99,
      stock: 28,
      rating: 4.8,
      reviews: 89,
      isFeatured: true,
      isInfluencerChoice: true,
      image: '/images/products/whey.jpg',
      status: 'Aktif'
    },
    {
      id: "3",
      name: 'Pro Gym Eldivenleri',
      category: 'Aksesuar',
      price: 149.99,
      stock: 12,
      rating: 4.6,
      reviews: 45,
      isFeatured: false,
      isInfluencerChoice: false,
      image: '/images/products/gloves.jpg',
      status: 'Düşük Stok'
    }
  ]);

  const [dialogOpen, setDialogOpen] = useState(false);
  const [editingProduct, setEditingProduct] = useState<any>(null);
  const [stockDialogOpen, setStockDialogOpen] = useState(false);
  const [priceDialogOpen, setPriceDialogOpen] = useState(false);
  const [selectedProduct, setSelectedProduct] = useState<any>(null);
  const [newStock, setNewStock] = useState(0);
  const [newPrice, setNewPrice] = useState(0);

  const [formData, setFormData] = useState({
    name: '',
    category: '',
    price: 0,
    stock: 0,
    description: '',
    image: '',
    isFeatured: false,
    isInfluencerChoice: false,
    influencerComment: ''
  });

  const categories = ['Giyim', 'Supplement', 'Aksesuar', 'Ekipman'];

  const handleEdit = (product: any) => {
    setEditingProduct(product);
    setFormData({
      name: product.name,
      category: product.category,
      price: product.price,
      stock: product.stock,
      description: product.description || '',
      image: product.image,
      isFeatured: product.isFeatured,
      isInfluencerChoice: product.isInfluencerChoice,
      influencerComment: product.influencerComment || ''
    });
    setDialogOpen(true);
  };

  const handleCreate = () => {
    setEditingProduct(null);
    setFormData({
      name: '',
      category: '',
      price: 0,
      stock: 0,
      description: '',
      image: '',
      isFeatured: false,
      isInfluencerChoice: false,
      influencerComment: ''
    });
    setDialogOpen(true);
  };

  const handleSave = () => {
    if (editingProduct) {
      // Update existing product
      setProducts(products.map(p => 
        p.id === editingProduct.id 
          ? { ...p, ...formData }
          : p
      ));
    } else {
      // Create new product
      const newProduct = {
        id: (Math.max(...products.map(p => parseInt(p.id))) + 1).toString(),
        ...formData,
        rating: 5.0,
        reviews: 0,
        status: 'Aktif'
      };
      setProducts([...products, newProduct]);
    }
    setDialogOpen(false);
  };

  const handleDelete = (id: string) => {
    if (window.confirm('Bu ürünü silmek istediğinizden emin misiniz?')) {
      setProducts(products.filter(p => p.id !== id));
    }
  };

  const handleStockUpdate = () => {
    if (selectedProduct) {
      setProducts(products.map(p => 
        p.id === selectedProduct.id 
          ? { ...p, stock: newStock, status: newStock < 15 ? 'Düşük Stok' : 'Aktif' }
          : p
      ));
      setStockDialogOpen(false);
    }
  };

  const handlePriceUpdate = () => {
    if (selectedProduct) {
      setProducts(products.map(p => 
        p.id === selectedProduct.id 
          ? { ...p, price: newPrice }
          : p
      ));
      setPriceDialogOpen(false);
    }
  };

  const getStatusColor = (status: string) => {
    switch (status) {
      case 'Aktif': return 'success';
      case 'Düşük Stok': return 'warning';
      case 'Stokta Yok': return 'error';
      default: return 'default';
    }
  };

  return (
    <Container maxWidth="xl" sx={{ py: 4 }}>
      <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 4 }}>
        <Typography variant="h4" sx={{ fontWeight: 700, color: '#FF6B35' }}>
          🛍️ Ürün Yönetimi
        </Typography>
        <Button
          variant="contained"
          startIcon={<Add />}
          onClick={handleCreate}
          sx={{
            bgcolor: '#FF6B35',
            '&:hover': { bgcolor: '#E55A2B' }
          }}
        >
          Yeni Ürün Ekle
        </Button>
      </Box>

      {/* İstatistik Kartları */}
      <Grid container spacing={3} sx={{ mb: 4 }}>
        <Grid xs={12} sm={6} md={3}>
          <Card>
            <CardContent>
              <Box sx={{ display: 'flex', alignItems: 'center' }}>
                <Box sx={{ bgcolor: '#FF6B35', color: 'white', borderRadius: 2, p: 1, mr: 2 }}>
                  <Inventory />
                </Box>
                <Box>
                  <Typography variant="h5">{products.length}</Typography>
                  <Typography variant="body2" color="text.secondary">Toplam Ürün</Typography>
                </Box>
              </Box>
            </CardContent>
          </Card>
        </Grid>
        <Grid xs={12} sm={6} md={3}>
          <Card>
            <CardContent>
              <Box sx={{ display: 'flex', alignItems: 'center' }}>
                <Box sx={{ bgcolor: '#27AE60', color: 'white', borderRadius: 2, p: 1, mr: 2 }}>
                  <Star />
                </Box>
                <Box>
                  <Typography variant="h5">{products.filter(p => p.isFeatured).length}</Typography>
                  <Typography variant="body2" color="text.secondary">Öne Çıkan</Typography>
                </Box>
              </Box>
            </CardContent>
          </Card>
        </Grid>
        <Grid xs={12} sm={6} md={3}>
          <Card>
            <CardContent>
              <Box sx={{ display: 'flex', alignItems: 'center' }}>
                <Box sx={{ bgcolor: '#F39C12', color: 'white', borderRadius: 2, p: 1, mr: 2 }}>
                  <Category />
                </Box>
                <Box>
                  <Typography variant="h5">{new Set(products.map(p => p.category)).size}</Typography>
                  <Typography variant="body2" color="text.secondary">Kategori</Typography>
                </Box>
              </Box>
            </CardContent>
          </Card>
        </Grid>
        <Grid xs={12} sm={6} md={3}>
          <Card>
            <CardContent>
              <Box sx={{ display: 'flex', alignItems: 'center' }}>
                <Box sx={{ bgcolor: '#E74C3C', color: 'white', borderRadius: 2, p: 1, mr: 2 }}>
                  <AttachMoney />
                </Box>
                <Box>
                  <Typography variant="h5">
                    ₺{products.reduce((sum, p) => sum + (p.price * p.stock), 0).toLocaleString()}
                  </Typography>
                  <Typography variant="body2" color="text.secondary">Stok Değeri</Typography>
                </Box>
              </Box>
            </CardContent>
          </Card>
        </Grid>
      </Grid>

      {/* Ürün Tablosu */}
      <Paper sx={{ borderRadius: 3, overflow: 'hidden' }}>
        <Box sx={{ p: 3, bgcolor: '#F8F9FA', borderBottom: '1px solid #E0E0E0' }}>
          <Typography variant="h6" sx={{ fontWeight: 600 }}>
            Ürün Listesi
          </Typography>
        </Box>
        
        <TableContainer>
          <Table>
            <TableHead>
              <TableRow sx={{ bgcolor: '#FFF5F0' }}>
                <TableCell sx={{ fontWeight: 600 }}>Ürün</TableCell>
                <TableCell sx={{ fontWeight: 600 }}>Kategori</TableCell>
                <TableCell sx={{ fontWeight: 600 }}>Fiyat</TableCell>
                <TableCell sx={{ fontWeight: 600 }}>Stok</TableCell>
                <TableCell sx={{ fontWeight: 600 }}>Rating</TableCell>
                <TableCell sx={{ fontWeight: 600 }}>Durum</TableCell>
                <TableCell sx={{ fontWeight: 600 }}>İşlemler</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {products.map((product) => (
                <TableRow key={product.id} sx={{ '&:hover': { bgcolor: '#FFFAF5' } }}>
                  <TableCell>
                    <Box sx={{ display: 'flex', alignItems: 'center' }}>
                      <img
                        src={product.image}
                        alt={product.name}
                        style={{
                          width: 50,
                          height: 50,
                          borderRadius: 8,
                          objectFit: 'cover',
                          marginRight: 12
                        }}
                      />
                      <Box>
                        <Typography variant="body2" sx={{ fontWeight: 600 }}>
                          {product.name}
                        </Typography>
                        <Box sx={{ display: 'flex', gap: 1, mt: 0.5 }}>
                          {product.isFeatured && (
                            <Chip label="Öne Çıkan" size="small" color="primary" />
                          )}
                          {product.isInfluencerChoice && (
                            <Chip label="⭐ Influencer" size="small" sx={{ bgcolor: '#FFA500', color: 'white' }} />
                          )}
                        </Box>
                      </Box>
                    </Box>
                  </TableCell>
                  <TableCell>
                    <Chip label={product.category} variant="outlined" />
                  </TableCell>
                  <TableCell>
                    <Typography variant="body2" sx={{ fontWeight: 600 }}>
                      ₺{product.price.toFixed(2)}
                    </Typography>
                  </TableCell>
                  <TableCell>
                    <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
                      <Typography variant="body2">{product.stock}</Typography>
                      <IconButton
                        size="small"
                        onClick={() => {
                          setSelectedProduct(product);
                          setNewStock(product.stock);
                          setStockDialogOpen(true);
                        }}
                      >
                        <Edit fontSize="small" />
                      </IconButton>
                    </Box>
                  </TableCell>
                  <TableCell>
                    <Box sx={{ display: 'flex', alignItems: 'center' }}>
                      <Star sx={{ color: '#FFA500', fontSize: 16, mr: 0.5 }} />
                      <Typography variant="body2">
                        {product.rating} ({product.reviews})
                      </Typography>
                    </Box>
                  </TableCell>
                  <TableCell>
                    <Chip 
                      label={product.status}
                      color={getStatusColor(product.status) as any}
                      size="small"
                      variant="outlined"
                    />
                  </TableCell>
                  <TableCell>
                    <Box sx={{ display: 'flex', gap: 1 }}>
                      <IconButton size="small" sx={{ color: '#FF6B35' }}>
                        <Visibility />
                      </IconButton>
                      <IconButton 
                        size="small" 
                        sx={{ color: '#27AE60' }}
                        onClick={() => handleEdit(product)}
                      >
                        <Edit />
                      </IconButton>
                      <IconButton
                        size="small"
                        onClick={() => {
                          setSelectedProduct(product);
                          setNewPrice(product.price);
                          setPriceDialogOpen(true);
                        }}
                        sx={{ color: '#F39C12' }}
                      >
                        <AttachMoney />
                      </IconButton>
                      <IconButton 
                        size="small" 
                        sx={{ color: '#E74C3C' }}
                        onClick={() => handleDelete(product.id)}
                      >
                        <Delete />
                      </IconButton>
                    </Box>
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      </Paper>

      {/* Ürün Ekleme/Düzenleme Dialog */}
      <Dialog open={dialogOpen} onClose={() => setDialogOpen(false)} maxWidth="md" fullWidth>
        <DialogTitle sx={{ bgcolor: '#FF6B35', color: 'white' }}>
          {editingProduct ? 'Ürün Düzenle' : 'Yeni Ürün Ekle'}
        </DialogTitle>
        <DialogContent sx={{ p: 3 }}>
          <Grid container spacing={3} sx={{ mt: 1 }}>
            <Grid xs={12} md={6}>
              <TextField
                fullWidth
                label="Ürün Adı"
                value={formData.name}
                onChange={(e) => setFormData({...formData, name: e.target.value})}
              />
            </Grid>
            <Grid xs={12} md={6}>
              <FormControl fullWidth>
                <InputLabel>Kategori</InputLabel>
                <Select
                  value={formData.category}
                  onChange={(e) => setFormData({...formData, category: e.target.value})}
                  label="Kategori"
                >
                  {categories.map((cat) => (
                    <MenuItem key={cat} value={cat}>{cat}</MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Grid>
            <Grid xs={12} md={6}>
              <TextField
                fullWidth
                label="Fiyat (₺)"
                type="number"
                value={formData.price}
                onChange={(e) => setFormData({...formData, price: parseFloat(e.target.value)})}
              />
            </Grid>
            <Grid xs={12} md={6}>
              <TextField
                fullWidth
                label="Stok Miktarı"
                type="number"
                value={formData.stock}
                onChange={(e) => setFormData({...formData, stock: parseInt(e.target.value)})}
              />
            </Grid>
            <Grid xs={12}>
              <TextField
                fullWidth
                label="Açıklama"
                multiline
                rows={3}
                value={formData.description}
                onChange={(e) => setFormData({...formData, description: e.target.value})}
              />
            </Grid>
            <Grid xs={12}>
              <TextField
                fullWidth
                label="Resim URL"
                value={formData.image}
                onChange={(e) => setFormData({...formData, image: e.target.value})}
              />
            </Grid>
            <Grid xs={12} md={6}>
              <FormControlLabel
                control={
                  <Switch
                    checked={formData.isFeatured}
                    onChange={(e) => setFormData({...formData, isFeatured: e.target.checked})}
                  />
                }
                label="Öne Çıkan Ürün"
              />
            </Grid>
            <Grid xs={12} md={6}>
              <FormControlLabel
                control={
                  <Switch
                    checked={formData.isInfluencerChoice}
                    onChange={(e) => setFormData({...formData, isInfluencerChoice: e.target.checked})}
                  />
                }
                label="Influencer Seçimi"
              />
            </Grid>
            {formData.isInfluencerChoice && (
              <Grid xs={12}>
                <TextField
                  fullWidth
                  label="Influencer Yorumu"
                  value={formData.influencerComment}
                  onChange={(e) => setFormData({...formData, influencerComment: e.target.value})}
                />
              </Grid>
            )}
          </Grid>
        </DialogContent>
        <DialogActions sx={{ p: 3 }}>
          <Button onClick={() => setDialogOpen(false)}>İptal</Button>
          <Button 
            variant="contained" 
            onClick={handleSave}
            sx={{ bgcolor: '#FF6B35', '&:hover': { bgcolor: '#E55A2B' } }}
          >
            {editingProduct ? 'Güncelle' : 'Oluştur'}
          </Button>
        </DialogActions>
      </Dialog>

      {/* Stok Güncelleme Dialog */}
      <Dialog open={stockDialogOpen} onClose={() => setStockDialogOpen(false)}>
        <DialogTitle>Stok Güncelle</DialogTitle>
        <DialogContent>
          <TextField
            fullWidth
            label="Yeni Stok Miktarı"
            type="number"
            value={newStock}
            onChange={(e) => setNewStock(parseInt(e.target.value))}
            sx={{ mt: 2 }}
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setStockDialogOpen(false)}>İptal</Button>
          <Button variant="contained" onClick={handleStockUpdate}>Güncelle</Button>
        </DialogActions>
      </Dialog>

      {/* Fiyat Güncelleme Dialog */}
      <Dialog open={priceDialogOpen} onClose={() => setPriceDialogOpen(false)}>
        <DialogTitle>Fiyat Güncelle</DialogTitle>
        <DialogContent>
          <TextField
            fullWidth
            label="Yeni Fiyat (₺)"
            type="number"
            value={newPrice}
            onChange={(e) => setNewPrice(parseFloat(e.target.value))}
            sx={{ mt: 2 }}
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setPriceDialogOpen(false)}>İptal</Button>
          <Button variant="contained" onClick={handlePriceUpdate}>Güncelle</Button>
        </DialogActions>
      </Dialog>
    </Container>
  );
};

export default AdminProducts;
